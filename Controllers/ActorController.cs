using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private readonly MovieTrackerContext _movieTrackerContext;

    public ActorController(MovieTrackerContext movieTrackerContext)
    {
        _movieTrackerContext = movieTrackerContext;
    }
    
    [HttpGet(Name = "GetActor")]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _movieTrackerContext.Actors.ToListAsync());
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