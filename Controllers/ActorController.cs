using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private readonly IActorService _service;

    public ActorController(IActorService service)
    {
        _service = service;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync() => 
        Ok(await _service.GetAllAsync());

    [HttpGet("by-id/{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) => 
        Ok(await _service.GetByIdAsync(id));

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(Actor actor) => 
        Created($"/GetById?id={(await _service.CreateAsync(actor))?.Id}", actor);

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Actor actor) =>
        await _service.UpdateAsync(actor) ? NoContent() : NotFound();

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();
}