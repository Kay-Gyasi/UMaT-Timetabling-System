using OneOf.Types;

namespace UMaTLMS.API.Controllers;

public class TimetableController : Controller
{
    private readonly TimetableProcessor _processor;

    public TimetableController(TimetableProcessor processor)
    {
        _processor = processor;
    }

    [HttpGet]
    //[Authorize("")] For developers and chief examiners only
    public async Task<IActionResult> Generate()
    {
        var result = await _processor.Generate();
        if (result.IsT1) return new ObjectResult(ErrorResponse(result.AsT1));
        return new ObjectResult(SuccessResponse<object>(null));
    }
    
    [HttpGet]
    //[Authorize("")] For developers and chief examiners only
    public async Task<IActionResult> GenerateLectures()
    {
        var result = await _processor.GenerateLectures();
        if (result.IsT0) return new ObjectResult(SuccessResponse(result.AsT0));
        return new ObjectResult(ErrorResponse(result.AsT1));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        var result = await _processor.SyncWithUMaT();
        if (result.IsT0) return new ObjectResult(SuccessResponse(result.AsT0));
        return new ObjectResult(ErrorResponse(result.AsT1));
    }
}