namespace MovieTracker.Models.Entities;

public class Series
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public virtual ICollection<Episode>? Episodes { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }

    //EF required
    public Series() { }
    public Series(string title, bool watched, string? description = null, byte? score = null, ICollection<Episode>? episodes = null, ICollection<Role>? roles = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Watched = watched;
        Description = description;
        Score = score;
        Episodes = episodes;
        Roles = roles;
        
        UpdateEpisodeWatched();
    }

    public void UpdateEpisodeWatched()
    {
        if (!Watched || Episodes == null) 
            return;
        foreach (var episode in Episodes)
        {
            episode.Watched = true;
        }
    }
}