using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Models.Entities;
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

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(RoleModel role, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _repository.CreateAsync(role, cancellationToken))?.Id}", role);

    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, RoleModel role, CancellationToken cancellationToken) =>
        await _repository.UpdateAsync(id, role, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}