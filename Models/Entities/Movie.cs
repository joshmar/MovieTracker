namespace MovieTracker.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public virtual ICollection<Role>? Roles { get; set; }
}