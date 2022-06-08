using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeService _service;

    public EpisodeController(IEpisodeService service)
    {
        _service = service;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) => 
        Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("by-id/{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        Ok(await _service.GetByIdAsync(id, cancellationToken));

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(Episode episode, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _service.CreateAsync(episode, cancellationToken))?.Id}", episode);

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Episode episode, CancellationToken cancellationToken) =>
        await _service.UpdateAsync(episode, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}