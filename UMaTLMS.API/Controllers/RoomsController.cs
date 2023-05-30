namespace UMaTLMS.API.Controllers;

public class RoomsController : Controller
{
    private readonly RoomProcessor _processor;

    public RoomsController(RoomProcessor processor)
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

    [HttpGet]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        return Ok(await _processor.GetPageAsync(command));
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] RoomCommand command)
    {
        var result = await _processor.UpsertAsync(command);
        return result.IsT0
            ? new ObjectResult(SuccessResponse(result.AsT0))
            : new ObjectResult(ErrorResponse(result.AsT1));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _processor.DeleteAsync(id);
        return new ObjectResult(SuccessResponse<object>(null));
    }
}