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
        return File(result.Item1, result.Item2, result.Item3);
    }
    
    [HttpGet]
    //[Authorize("")] For developers and chief examiners only
    public async Task<IActionResult> GenerateLectures()
    {
        await _processor.GenerateLectures();
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        await _processor.SyncWithUMaT();
        return NoContent();
    }
}