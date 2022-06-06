namespace MovieTracker.Models.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public Guid ActorId { get; set; }
    public Actor Actor { get; set; }
    
    public Guid? EpisodeId { get; set; }
    public Episode? Episode { get; set; }
    
    public Guid? MovieId { get; set; }
    public Movie? Movie { get; set; }
    
    public Guid? SeriesId { get; set; }
    public Series? Series { get; set; }

    public Role(string name, Actor actor, Episode episode, string? description = null, byte? score = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        ActorId = actor.Id;
        Actor = actor;
        Description = description;
        Score = score;
        Episode = episode;
        EpisodeId = episode.Id;
    }
    
    public Role(string name, Actor actor, Movie movie, string? description = null, byte? score = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        ActorId = actor.Id;
        Actor = actor;
        Description = description;
        Score = score;
        Movie = movie;
        MovieId = movie.Id;
    }
    
    public Role(string name, Actor actor, Series series, string? description = null, byte? score = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        ActorId = actor.Id;
        Actor = actor;
        Description = description;
        Score = score;
        Series = series;
        SeriesId = series.Id;
    }
}