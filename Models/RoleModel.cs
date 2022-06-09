namespace MovieTracker.Models;

public class RoleModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    public Guid? ActorId { get; set; }
    public Guid? EpisodeId { get; set; }
    public Guid? MovieId { get; set; }
    public Guid? SeriesId { get; set; }
}