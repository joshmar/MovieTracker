using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public Guid ActorId { get; set; }
    public Actor Actor { get; set; }
    
    public Guid? EpisodeId { get; set; }
    public Episode? Episode { get; set; }
    
    public Guid? MovieId { get; set; }
    public Movie? Movie { get; set; }
    
    public Guid? SeriesId { get; set; }
    public Series? Series { get; set; }
}