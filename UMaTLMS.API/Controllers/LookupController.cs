namespace UMaTLMS.API.Controllers;

public class LookupController : Controller
{
    private readonly LookupProcessor _processor;

    public LookupController(LookupProcessor processor)
    {
        _processor = processor;
    }

    [HttpGet("{type}")]
    public async Task<IActionResult> Get(LookupType type)
    {
        var result = await _processor.GetAsync(type);
        return Ok(SuccessResponse(result));
    }
}
