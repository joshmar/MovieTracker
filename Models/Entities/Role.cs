namespace MovieTracker.Models;

public class Role
{
    public Guid Id { get; set; }
    public Guid ActorId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    public List<RoleActor> RoleActors { get; set; }
    public List<RoleEpisode> RoleEpisodes { get; set; }
    public List<RoleSeries> RoleSeries { get; set; }
    public List<RoleMovie> RoleMovies { get; set; }
}