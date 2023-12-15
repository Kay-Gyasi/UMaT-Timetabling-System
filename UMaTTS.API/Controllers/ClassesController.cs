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

    [HttpGet("{classGroupId}")]
    public async Task<IActionResult> GetSubClassGroups(int classGroupId)
    {
        var result = await _processor.GetSubClassGroups(classGroupId);
        return new ObjectResult(SuccessResponse(result));
    }

    [HttpPost]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        var result = await _processor.GetPageAsync(command);
        return new ObjectResult(SuccessResponse(result));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateSubClasses([FromBody] List<SubClassGroupCommand> commands)
    {
        var result = await _processor.UpdateSubClasses(commands);
        return result.IsT0 && result.AsT0 == true
            ? new ObjectResult(SuccessResponse(result.AsT0, StatusCodes.Status204NoContent))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }

    [HttpPut("{classGroupId}/{numberOfSubClasses}")]
    public async Task<IActionResult> SetNumberOfSubClasses(int classGroupId, int numberOfSubClasses)
    {
        var result = await _processor.SetNumberOfSubClasses(numberOfSubClasses, classGroupId);
        return result.IsT0 && result.AsT0 == true
            ? new ObjectResult(SuccessResponse(result.AsT0, StatusCodes.Status204NoContent))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }

    [HttpPut("{classGroupId}/{classSize}")]
    public async Task<IActionResult> SetClassSize(int classGroupId, int classSize)
    {
        var result = await _processor.SetClassSize(classSize, classGroupId);
        return result.IsT0 && result.AsT0 == true
            ? new ObjectResult(SuccessResponse(result.AsT0, StatusCodes.Status204NoContent))
            : new ObjectResult(ErrorResponse(result.AsT1));

    }

    [HttpPut("{limit}")]
    public async Task<IActionResult> SetLimit(int limit)
    {
        var result = await _processor.SetLimit(limit);
        if (result.IsT1) return new ObjectResult(ErrorResponse(result.AsT1));
        return new ObjectResult(SuccessResponse<object>(null));
    }
}
