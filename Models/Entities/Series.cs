namespace MovieTracker.Models;

public class Series
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    public List<SeriesEpisode> SeriesEpisodes { get; set; }
    public List<RoleSeries> RoleSeries { get; set; }
}