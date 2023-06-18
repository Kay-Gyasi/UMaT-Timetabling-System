using Microsoft.AspNetCore.Builder;
using UMaTLMS.Core.Services;
using UMaTLMS.Infrastructure.Persistence.Repositories;

namespace UMaTLMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterDbContext(configuration)
            .RegisterRepositories()
            .AddHttpClient("UMaT", opts =>
            {
                opts.BaseAddress = new Uri("https://sys.umat.edu.gh/dev/api/");
                opts.Timeout = TimeSpan.FromMinutes(5);
            });
            services.AddScoped<IUMaTApiService, UMaTApiService>()
            .AddScoped<IExcelReader, ExcelReader>();
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
        IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<AuditEntitiesInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            var saveChangesInterceptor = sp.GetService<AuditEntitiesInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString("Default"), o =>
                {
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                })
                .AddInterceptors(interceptor!, saveChangesInterceptor!);
            options.EnableSensitiveDataLogging();
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
            .AddScoped<IOnlineLectureScheduleRepository, OnlineLectureScheduleRepository>();
        return services;
    }
}