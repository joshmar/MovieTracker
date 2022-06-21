using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private readonly IActorRepository _repository;

    public ActorController(IActorRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) => 
        Ok((await _repository.GetAllAsync(cancellationToken))
            .Select(x => x));

    [HttpGet("by-id/{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        Ok(await _repository.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ActorModel actor, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _repository.CreateAsync(actor, cancellationToken))?.Id}", actor);

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, ActorModel actor, CancellationToken cancellationToken) =>
        await _repository.UpdateAsync(id, actor, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}