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
        await _processor.Generate();
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        await _processor.SeedDbForTimetable();
        return NoContent();
    }

    [HttpGet("{classId}")]
    public async Task<IActionResult> GetClassSchedule(int classId)
    {
        return new ObjectResult(SuccessResponse(await _processor.GetClassSchedule(classId)));
    }
}