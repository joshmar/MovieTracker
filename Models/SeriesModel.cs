namespace MovieTracker.Models;

public class SeriesModel
{
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
}