using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;

namespace MovieTracker;

public class MovieTrackerContext : DbContext
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Serie> Series { get; set; }

    public MovieTrackerContext(DbContextOptions<MovieTrackerContext> options)
        : base(options) { }
}