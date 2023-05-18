using UMaTLMS.Core.Authentication;
using UMaTLMS.Core.Services;
using UMaTLMS.Infrastructure.Persistence.Repositories;

namespace UMaTLMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterDbContext(configuration)
            .RegisterRepositories();

        services.AddScoped<ITokenService, TokenService>()
            .AddScoped<IExcelReader, ExcelReader>();
        return services;
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
            options.UseSqlServer(configuration.GetConnectionString("Default"))
                .AddInterceptors(interceptor!, saveChangesInterceptor!);
            options.EnableSensitiveDataLogging();
        });

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>()
            .AddScoped<ISemesterRepository, SemesterRepository>()
            .AddScoped<IRoomRepository, RoomRepository>()
            .AddScoped<ILecturerRepository, LecturerRepository>()
            .AddScoped<IDepartmentRepository, DepartmentRepository>()
            .AddScoped<ICourseRepository, CourseRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IClassRepository, ClassRepository>();

        return services;
    }
}