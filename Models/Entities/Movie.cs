﻿namespace MovieTracker.Models.Entities;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public virtual ICollection<Role>? Roles { get; set; }

    //EF required
    public Movie() { }
    public Movie(string title, bool watched, string? description = null, byte? score = null, ICollection<Role>? roles = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Watched = watched;
        Description = description;
        Score = score;
        Roles = roles;
    }
}