namespace MovieTracker.Models;

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
}