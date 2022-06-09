namespace MovieTracker.Models;

public class MovieModel
{
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
}