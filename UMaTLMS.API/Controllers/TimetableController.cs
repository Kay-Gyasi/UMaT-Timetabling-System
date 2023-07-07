namespace UMaTLMS.API.Controllers;

public class TimetableController : Controller
{
    private readonly TimetableProcessor _processor;
    private readonly ExamsScheduleProcessor _examsScheduleProcessor;

    public TimetableController(TimetableProcessor processor, ExamsScheduleProcessor examsScheduleProcessor)
    {
        _processor = processor;
        _examsScheduleProcessor = examsScheduleProcessor;
    }

    [HttpGet]
    //[Authorize("")] For developers and chief examiners only
    public async Task<IActionResult> Generate()
    {
        var result = await _processor.Generate();
        return result.IsT1 ? new ObjectResult(ErrorResponse(result.AsT1)) 
            : new ObjectResult(SuccessResponse<object>(null));
    }
    
    [HttpPost]
    //[Authorize("")] For developers and chief examiners only
    public async Task<IActionResult> Generate(ExamsScheduleCommand command)
    {
        var result = await _examsScheduleProcessor.Generate(command);
        return result.IsT1 ? new ObjectResult(ErrorResponse(result.AsT1)) 
            : new ObjectResult(SuccessResponse<object>(null));
    }
    
    [HttpGet]
    //[Authorize("")] For developers and chief examiners only
    public async Task<IActionResult> GenerateLectures()
    {
        var result = await _processor.GenerateLectures();
        return result.IsT0 ? new ObjectResult(SuccessResponse(result.AsT0)) 
            : new ObjectResult(ErrorResponse(result.AsT1));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        var result = await _processor.SyncWithUMaT();
        return result.IsT0 ? new ObjectResult(SuccessResponse(result.AsT0)) 
            : new ObjectResult(ErrorResponse(result.AsT1));
    }
}