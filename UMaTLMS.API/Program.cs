var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.File("_logs/logs.txt", LogEventLevel.Warning)
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.RegisterServices(builder.Configuration);

WebApplication app;
try
{
    logger.Information("App starting...");
    app = builder.BuildApp(logger);
    logger.Information("App running!");
}
catch (Exception e)
{
    logger.Error("{Message}", e.Message);
    throw;
}

app.ConfigurePipeline();