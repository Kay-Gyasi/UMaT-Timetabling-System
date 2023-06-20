using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UMaTLMS.Core.Processors;

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

    public static void Initialize(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var initializer = serviceScope.ServiceProvider.GetService<Initializer>();
        initializer?.Initialize().GetAwaiter().GetResult();
    }
}