namespace UMaTLMS.API.Controllers;

public class PreferencesController : Controller
{
    private readonly PreferenceProcessor _processor;

    public PreferencesController(PreferenceProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    public async Task<IActionResult> Set([FromBody] PreferenceCommand command)
    {
        var result = await _processor.Set(command);
        if (result.IsT1) return new ObjectResult(ErrorResponse(result.AsT1));
        return new ObjectResult(SuccessResponse(result.AsT0));
    }

    [HttpGet]
    [ProducesDefaultResponseType(typeof(PreferenceLookups))]
    public async Task<IActionResult> GetLookups()
    {
        var lookups = await Task.Run(() => PreferenceProcessor.GetTypes());
        return new ObjectResult(SuccessResponse(lookups));
    }
    
    [HttpGet("{type}")]
    [ProducesDefaultResponseType(typeof(List<Lookup>))]
    public async Task<IActionResult> GetTypeValues(int type)
    {
        var values = await Task.Run(() => _processor.GetTypeValues(type));
        return new ObjectResult(SuccessResponse(values));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _processor.DeleteAsync(id);
        return new ObjectResult(SuccessResponse<object>(null));
    }
}
