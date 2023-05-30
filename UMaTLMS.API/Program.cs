var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
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