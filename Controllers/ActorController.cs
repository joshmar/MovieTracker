using Microsoft.AspNetCore.Mvc;
using MovieTracker.Extension;
using MovieTracker.Models;

namespace MovieTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private IEnumerable<Actor> _actor = new List<Actor>
    {
        new Actor
        {
            Id = Guid.NewGuid(),
            FirstName = "Bruce",
            LastName = "Willis",
            Score = 9
        }
    };
    
    [HttpGet(Name = "GetActor")]
    public IActionResult Get()
    {
        return Ok(_actor);
    }

    [HttpPost(Name = "AddActor")]
    public IActionResult AddActor([FromBody]Actor actor)
    {
        if (!IsActorValid(actor))
        {
            return BadRequest("Invalid actor details.");
        }
        _actor.Append(actor);
        return Ok(actor);
    }

    private static bool IsActorValid(Actor actor) =>
        !actor.FirstName.IsNullOrWhiteSpace() || !actor.LastName.IsNullOrWhiteSpace();
}