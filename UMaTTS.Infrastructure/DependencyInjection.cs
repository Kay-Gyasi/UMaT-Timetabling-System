using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UMaTLMS.Core.Helpers;
using UMaTLMS.Core.Services;
using UMaTLMS.Infrastructure.Persistence.Repositories;

namespace UMaTLMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.RegisterDbContext(configuration, hostEnvironment)
            .RegisterRepositories()
            .AddHttpClient("UMaT", opts =>
            {
                opts.BaseAddress = new Uri(configuration["UMaTAPI"] ?? string.Empty);
                opts.Timeout = TimeSpan.FromMinutes(5);
            });
            services.AddScoped<IUMaTApiService, UMaTApiService>()
            .AddScoped<IExcelReader, ExcelReader>()
            .AddMemoryCache()
            .AddScoped<CacheService>();
        return services;
    }

    public static void AddMigrations(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        if (Environment.CommandLine.Contains("migrations add")) return;
        context?.Database.Migrate();
    }

    private static IServiceCollection RegisterDbContext(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<AuditEntitiesInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            var saveChangesInterceptor = sp.GetService<AuditEntitiesInterceptor>();
            var dbName = hostEnvironment.IsProduction() ? "Default" : null;
            dbName = AppHelpers.HostEnvironment.IsDocker ? "DefaultDocker" : dbName;
            dbName ??= "DefaultDev";
            options.UseSqlServer(configuration.GetConnectionString(dbName), o =>
                {
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                })
                .AddInterceptors(interceptor!, saveChangesInterceptor!);
            if (hostEnvironment.IsDevelopment()) options.EnableSensitiveDataLogging();
        });

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<ICourseRepository, CourseRepository>()
            .AddScoped<IClassGroupRepository, ClassGroupRepository>()
            .AddScoped<ISubClassGroupRepository, SubClassGroupRepository>()
            .AddScoped<ILectureRepository, LectureRepository>()
            .AddScoped<ILecturerRepository, LecturerRepository>()
            .AddScoped<ILectureScheduleRepository, LectureScheduleRepository>()
            .AddScoped<IPreferenceRepository, PreferenceRepository>()
            .AddScoped<IConstraintRepository, ConstraintRepository>()
            .AddScoped<IOnlineLectureScheduleRepository, OnlineLectureScheduleRepository>();
        return services;
    }
}