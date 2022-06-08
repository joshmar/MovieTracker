namespace MovieTracker.Models.Entities;

public class Episode
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public Guid SeriesId { get; set; }
    public Series Series { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }

    //EF required
    public Episode() { }
    public Episode(string title, bool watched, Series series, string? description = null, byte? score = null, ICollection<Role>? roles = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Watched = watched;
        Series = series;
        SeriesId = series.Id;
        Description = description;
        Score = score;
        Roles = roles;
    }
    
    
}