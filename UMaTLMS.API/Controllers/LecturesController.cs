namespace UMaTLMS.API.Controllers;

public class LecturesController : Controller
{
    private readonly LectureProcessor _processor;

    public LecturesController(LectureProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(PaginatedList<LecturePageDto>))]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        var page = await _processor.GetPageAsync(command);
        return new ObjectResult(SuccessResponse(page));
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
    public async Task<IActionResult> CreateCombined(List<LectureCommand> lectures)
    {
        var result = await _processor.CreateCombined(lectures);
        return result.IsT0
            ? new ObjectResult(SuccessResponse(result.AsT0))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }
}
