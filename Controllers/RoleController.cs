using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
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
    public async Task<IActionResult> CreateAsync(Role role, CancellationToken cancellationToken) => 
        Created($"/GetById?id={(await _service.CreateAsync(role, cancellationToken))?.Id}", role);

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Role role, CancellationToken cancellationToken) =>
        await _service.UpdateAsync(role, cancellationToken) ? NoContent() : NotFound();*/

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}