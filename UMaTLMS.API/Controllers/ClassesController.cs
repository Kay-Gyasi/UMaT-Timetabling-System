namespace UMaTLMS.API.Controllers;

public class ClassesController : Controller
{
    private readonly ClassProcessor _processor;

    public ClassesController(ClassProcessor processor)
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
        var result = await _processor.GetPageAsync(command);
        return new ObjectResult(SuccessResponse(result));
    }

    [HttpPut("{classGroupId}/{numberOfSubClasses}")]
    public async Task<IActionResult> SetNumberOfSubClasses(int classGroupId, int numberOfSubClasses)
    {
        var result = await _processor.SetNumberOfSubClasses(numberOfSubClasses, classGroupId);
        return result.IsT0 && result.AsT0 == true
            ? new ObjectResult(SuccessResponse(result.AsT0, StatusCodes.Status204NoContent))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }
}
