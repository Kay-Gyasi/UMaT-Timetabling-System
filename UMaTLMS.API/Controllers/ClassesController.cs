﻿namespace UMaTLMS.API.Controllers;

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
        return Ok(await _processor.GetAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        return Ok(await _processor.GetPageAsync(command));
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ClassCommand command)
    {
        var result = await _processor.UpsertAsync(command);
        return CreatedAtRoute(nameof(Get), result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _processor.DeleteAsync(id);
        return NoContent();
    }
}