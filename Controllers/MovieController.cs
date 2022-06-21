using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _repository;

    public MovieController(IMovieRepository repository)
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
    public async Task<IActionResult> CreateAsync(MovieModel movie, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _repository.CreateAsync(movie, cancellationToken))?.Id}", movie);

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, MovieModel movie, CancellationToken cancellationToken) =>
        await _repository.UpdateAsync(id, movie, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}