using Serilog;

namespace UMaTLMS.API;

public static class RequestPipeline
{
    public static WebApplication BuildApp(this WebApplicationBuilder builder, Serilog.ILogger logger)
    {
        builder.Host.UseSerilog(logger);
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
        return builder.Build();
    }

    public static void ConfigurePipeline(this WebApplication app)
    {
        app.AddMigrations();
        app.UseSwagger();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseCors();
        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
