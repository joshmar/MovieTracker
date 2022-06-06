using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private readonly MovieTrackerContext _context;

    public ActorController(MovieTrackerContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "GetActor")]
    public async Task<IActionResult> GetAsync()
    {
        var actor = await _context.Actors.FirstAsync();
        
        var returnObject = new { 
            actor = actor.FirstName + actor.LastName, 
            roles = actor.Roles?.First(), 
            movie = actor.Roles?.First().Movie?.Title
        };

        return Ok(returnObject);
    }

    [HttpPost(Name = "AddActor")]
    public IActionResult AddActor([FromBody]Actor actor)
    {
        if (!IsActorValid(actor))
        {
            return BadRequest("Invalid actor details.");
        }
        return Ok(actor);
    }

    private static bool IsActorValid(Actor actor) =>
        !actor.FirstName.IsNullOrWhiteSpace() || !actor.LastName.IsNullOrWhiteSpace();
}