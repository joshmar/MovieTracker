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
    public virtual Series Series { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }

    [Obsolete("EF Required", true)]
    public Episode() { }
    public Episode(string title, bool watched, Guid seriesId, string? description = null, byte? score = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Watched = watched;
        SeriesId = seriesId;
        Description = description;
        Score = score;
    }


    public void Update(EpisodeModel updateModel)
    {
        Title = updateModel.Title;
        Watched = updateModel.Watched;
        SeriesId = updateModel.SeriesId;
        Description = updateModel.Description ?? Description;
        Score = updateModel.Score ?? Score;
    }
}