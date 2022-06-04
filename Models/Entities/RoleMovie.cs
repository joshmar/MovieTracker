namespace MovieTracker.Models;

public class RoleMovie
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}