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

    [Obsolete("EF Required", true)]
    public Series() { }
    public Series(string title, bool watched, string? description = null, byte? score = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Watched = watched;
        Description = description;
        Score = score;
    }
    
    public void Update(SeriesModel updateModel)
    {
        Title = updateModel.Title;
        Watched = updateModel.Watched;
        Description = updateModel.Description ?? Description;
        Score = updateModel.Score ?? Score;
    }
}