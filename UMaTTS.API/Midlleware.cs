using Microsoft.Extensions.FileProviders;
using Serilog;

namespace UMaTLMS.API;

public static class Midlleware
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
        app.Initialize();
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "UMaT Lecture Management API v1");
            if (!app.Environment.IsDevelopment()) options.RoutePrefix = string.Empty;
        });

        app.UseCors();
        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
            Path.Combine(app.Environment.ContentRootPath, "_content")),
            RequestPath = "/files",
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Append(
                     "Cache-Control", $"no-cache, no-store, must-revalidate");
            }
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
