namespace UMaTLMS.API.Controllers;

public class CoursesController : Controller
{
    private readonly CourseProcessor _processor;

    public CoursesController(CourseProcessor processor)
    {
        _processor = processor;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _processor.GetAsync(id);
        return result.IsT0
            ? new ObjectResult(SuccessResponse(result.AsT0))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }

    [HttpPost]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        var page = await _processor.GetPageAsync(command);
        return new ObjectResult(SuccessResponse(page));
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] CourseCommand command)
    {
        var result = await _processor.UpdateAsync(command);
        return result.IsT0
            ? new ObjectResult(SuccessResponse(result.AsT0))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(PaginatedList<PreferenceDto>))]
    public async Task<IActionResult> GetPreferences([FromBody] PaginatedCommand command)
    {
        var preferences = await _processor.GetPreferences(command);
        return new ObjectResult(SuccessResponse(preferences));
    }
}