﻿namespace MovieTracker.Models;

public class Serie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Watched { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
    public Guid[]? EpisodeIds { get; set; }
    public Guid[]? RoleIds { get; set; }
}