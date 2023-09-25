namespace UMaTLMS.API.Controllers;

public class LecturersController : Controller
{
    private readonly LecturerProcessor _processor;

    public LecturersController(LecturerProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] LecturerCommand command)
    {
        var result = await _processor.UpsertAsync(command);
        return result.IsT0
            ? new ObjectResult(SuccessResponse(result.AsT0, StatusCodes.Status201Created))
            : new ObjectResult(ErrorResponse(result.AsT1));
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _processor.DeleteAsync(id);
        return new ObjectResult(SuccessResponse<object>(null));
    }
}
