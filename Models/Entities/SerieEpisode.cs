namespace MovieTracker.Models;

public class SerieEpisode
{
    public Guid SerieId { get; set; }
    public Serie Serie { get; set; }

    public Guid EpisodeId { get; set; }
    public Episode Episode { get; set; }
}