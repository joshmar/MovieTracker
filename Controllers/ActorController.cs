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
        var actor = _context.Actors.First();
        var roles = await _context.Roles
            .SelectMany(role => role.RoleActors
                .Where(roleActor => roleActor.ActorId == actor.Id)
                .Select(x => x.Role))
            .FirstAsync();
        var movies = await _context.Movies
            .SelectMany(movie => movie.RoleMovies
                .Where(roleMovie => roleMovie.RoleId == roles.Id)
                .Select(roleMovie => roleMovie.Movie))
            .FirstAsync();
        
        var returnObject = new { actor = actor.FirstName + actor.LastName, roles = roles.Name, movie = movies.Title};

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