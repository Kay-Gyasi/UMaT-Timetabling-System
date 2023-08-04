namespace UMaTLMS.API.Controllers;

public class LecturersController : Controller
{
    private readonly LecturerProcessor _processor;

    public LecturersController(LecturerProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(PaginatedList<LecturerDto>))]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        var page = await _processor.GetPageAsync(command);
        return new ObjectResult(SuccessResponse(page));
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(PaginatedList<PreferenceDto>))]
    public async Task<IActionResult> GetPreferences([FromBody] PaginatedCommand command)
    {
        var preferences = await _processor.GetPreferences(command);
        return new ObjectResult(SuccessResponse(preferences));
    }
}
