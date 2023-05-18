using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace UMaTLMS.Infrastructure.Persistence.Interceptors;

public sealed class AuditEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuditEntitiesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var dbContext = eventData.Context;
        if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var userId = _httpContextAccessor.HttpContext?.User?
            .FindFirst(JwtRegisteredClaimNames.NameId)?.Value;
        var role = _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.Role)?.Value;
        var username = string.Join(" - ", role, userId);
        if (string.IsNullOrEmpty(username)) username = "admin";

        foreach (var entry in dbContext.ChangeTracker.Entries()
                     .Where(x => x.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.Entity is not Entity entity) continue;
            entity.Audit ??= Audit.Create();
            entity.Audit.WasCreatedBy(username);
            entity.Audit.Update(username);
        }

        foreach (var entry in dbContext.ChangeTracker.Entries()
                     .Where(x => x.State is EntityState.Deleted))
        {
            if (entry.Entity is not Entity entity) continue;
            entity.Audit?.Update(username);
            entity.Audit?.Delete();
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
