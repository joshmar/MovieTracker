﻿namespace MovieTracker.Models.Entities;

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
}