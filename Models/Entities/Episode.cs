namespace MovieTracker.Models;

public class Episode
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    public SerieEpisode Serie { get; set; }
    public List<RoleEpisode> RoleEpisodes { get; set; }
}