namespace UMaTLMS.API.Controllers;

public class UsersController : Controller
{
    private readonly UserProcessor _processor;

    public UsersController(UserProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return Ok(await _processor.LoginAsync(command));
    }
}