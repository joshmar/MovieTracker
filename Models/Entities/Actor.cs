﻿namespace MovieTracker.Models.Entities;

public class Actor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte? Score { get; set; }

    //Relationships
    public virtual ICollection<Role>? Roles { get; set; }
    
    [Obsolete("EF Required", true)]
    public Actor() { }
    public Actor(string firstName, string lastName, byte? score = null, ICollection<Role>? roles = null)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Score = score;
        Roles = roles;
    }
}