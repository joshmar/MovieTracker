namespace MovieTracker.Models;

public class EpisodeModel
{
    public string Title { get; set; }
    public bool Watched { get; set; }
    public Guid SeriesId { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
}