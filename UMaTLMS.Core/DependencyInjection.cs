using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UMaTLMS.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.InstallProcessors();
        services.AddMediatR(opts =>
        {
            opts.NotificationPublisher = new ForeachAwaitPublisher();
            opts.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        return services;
    }

    private static IServiceCollection InstallProcessors(this IServiceCollection services)
    {
        var processors = typeof(ProcessorAttribute).Assembly.DefinedTypes
            .Where(x => x.CustomAttributes.Any(a =>
                a.AttributeType == typeof(ProcessorAttribute)));

        foreach (var processor in processors)
        {
            services.AddScoped(processor);
        }
        return services;
    }
}