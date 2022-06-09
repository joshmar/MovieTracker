using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _service;

    public MovieController(IMovieService service)
    {
        _service = service;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) => 
        Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("by-id/{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        Ok(await _service.GetByIdAsync(id, cancellationToken));

    /*[HttpPost("create")]
    public async Task<IActionResult> CreateAsync(Movie movie, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _service.CreateAsync(movie, cancellationToken))?.Id}", movie);

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Movie movie, CancellationToken cancellationToken) =>
        await _service.UpdateAsync(movie, cancellationToken) ? NoContent() : NotFound();*/

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}