using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class SeriesController : ControllerBase
{
    private readonly ISeriesRepository _repository;

    public SeriesController(ISeriesRepository repository)
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
    public async Task<IActionResult> CreateAsync(SeriesModel series, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _repository.CreateAsync(series, cancellationToken))?.Id}", series);

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, SeriesModel series, CancellationToken cancellationToken) =>
        await _repository.UpdateAsync(id, series, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}