namespace UMaTLMS.API.Controllers;

public class InitializationController : Controller
{
    private readonly Initializer _initializer;

    public InitializationController(Initializer initializer)
    {
        _initializer = initializer;
    }

    [HttpGet]
    public async Task<IActionResult> Initialize()
    {
        await _initializer.Initialize();
        return new ObjectResult(SuccessResponse<object>(null));
    }
    
    [HttpGet]
    public async Task<IActionResult> Reset()
    {
        await _initializer.Reset();
        return new ObjectResult(SuccessResponse<object>(null));
    }
}
