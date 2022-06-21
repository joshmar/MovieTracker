using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _repository;

    public RoleController(IRoleRepository repository)
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
    public async Task<IActionResult> CreateAsync(RoleModel role, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _repository.CreateAsync(role, cancellationToken))?.Id}", role);

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, RoleModel role, CancellationToken cancellationToken) =>
        await _repository.UpdateAsync(id, role, cancellationToken) ? NoContent() : NotFound();
    
    [HttpPatch("addActorRelationship/{id:guid}/{actorId:guid}")]
    public async Task<IActionResult> AddActorRelationship(Guid id, Guid actorId, CancellationToken cancellationToken) =>
        Ok(await _repository.AddActorRelation(id, actorId, cancellationToken));
    
    [HttpPatch("addEpisodeRelationship/{id:guid}/{episodeId:guid}")]
    public async Task<IActionResult> AddEpisodeRelationship(Guid id, Guid episodeId, CancellationToken cancellationToken) =>
        Ok(await _repository.AddEpisodeRelation(id, episodeId, cancellationToken));
    
    [HttpPatch("addMovieRelationship/{id:guid}/{movieId:guid}")]
    public async Task<IActionResult> AddMovieRelationship(Guid id, Guid movieId, CancellationToken cancellationToken) =>
        Ok(await _repository.AddMovieRelation(id, movieId, cancellationToken));
    
    [HttpPatch("addSeriesRelationship/{id:guid}/{seriesId:guid}")]
    public async Task<IActionResult> AddSeriesRelationship(Guid id, Guid seriesId, RoleModel role, CancellationToken cancellationToken) =>
        Ok(await _repository.AddSeriesRelation(id, seriesId, cancellationToken));

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}