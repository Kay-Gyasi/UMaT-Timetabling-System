using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UMaTLMS.Core.Authentication;

namespace UMaTLMS.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationService(configuration)
            .InstallProcessors();
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