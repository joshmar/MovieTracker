using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeRepository _repository;

    public EpisodeController(IEpisodeRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) => 
        Ok(await _repository.GetAllAsync(cancellationToken));

    [HttpGet("by-id/{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        Ok(await _repository.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> CreateAsync(EpisodeModel episode, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _repository.CreateAsync(episode, cancellationToken))?.Id}", episode);

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, EpisodeModel episode, CancellationToken cancellationToken) =>
        await _repository.UpdateAsync(id, episode, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}