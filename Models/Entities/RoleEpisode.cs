namespace MovieTracker.Models;

public class RoleEpisode
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid EpisodeId { get; set; }
    public Episode Episode { get; set; }
}