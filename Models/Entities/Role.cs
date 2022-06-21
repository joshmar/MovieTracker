namespace MovieTracker.Models.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public Guid? ActorId { get; set; }
    public virtual Actor? Actor { get; set; }
    
    public Guid? EpisodeId { get; set; }
    public virtual Episode? Episode { get; set; }
    
    public Guid? MovieId { get; set; }
    public virtual Movie? Movie { get; set; }
    
    public Guid? SeriesId { get; set; }
    public virtual Series? Series { get; set; }

    [Obsolete("EF Required", true)]
    public Role() { }
    public Role(string name, Guid? actorId, Guid? episodeId, Guid? seriesId, Guid? movieId, string? description = null, byte? score = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        ActorId = actorId;
        Description = description;
        Score = score;
        EpisodeId = episodeId;
        SeriesId = seriesId;
        MovieId = movieId;
    }

    public void Update(RoleModel roleModel)
    {
        Name = roleModel.Name;
        Description = roleModel.Description ?? Description;
        Score = roleModel.Score ?? Score;
        ActorId = roleModel.ActorId ?? ActorId;
        EpisodeId = roleModel.EpisodeId ?? EpisodeId;
        MovieId = roleModel.MovieId ?? MovieId;
        SeriesId = roleModel.SeriesId ?? SeriesId;
    }
}