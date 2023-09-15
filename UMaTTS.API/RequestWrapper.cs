namespace UMaTLMS.API;

public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
            _logger.LogError("Error: {Error}", ex.ToString());
            var responseObj = new ApiErrorResponse(500, "One or more errors occurred.", ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(responseObj);
        }
    }
}
