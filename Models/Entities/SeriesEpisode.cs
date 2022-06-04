namespace MovieTracker.Models;

public class SeriesEpisode
{
    public Guid SeriesId { get; set; }
    public Series Series { get; set; }

    public Guid EpisodeId { get; set; }
    public Episode Episode { get; set; }
}