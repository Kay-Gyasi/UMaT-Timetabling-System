using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace UMaTLMS.Infrastructure.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var dbContext = eventData.Context;
        if (dbContext is null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var messages = dbContext.ChangeTracker
            .Entries<Entity>()
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                var events = new List<IDomainEvent>();
                events.AddRange(x.DomainEvents);
                x.ClearDomainEvents();
                return events.AsEnumerable();
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = domainEvent.Id,
                DateOccurred = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            }).ToList();

        foreach (var outboxMessage in messages)
        {
            await dbContext.Set<OutboxMessage>().AddAsync(outboxMessage, cancellationToken)
                .ConfigureAwait(false);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
