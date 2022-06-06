using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;

namespace MovieTracker;

public class MovieTrackerContext : DbContext
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Series> Series { get; set; }

    public MovieTrackerContext(DbContextOptions<MovieTrackerContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasOne<Actor>(role => role.Actor)
            .WithMany(actor => actor.Roles)
            .HasForeignKey(role => role.ActorId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<Role>()
            .HasOne<Series>(role => role.Series)
            .WithMany(series => series.Roles)
            .HasForeignKey(role => role.SeriesId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>()
            .HasOne<Movie>(role => role.Movie)
            .WithMany(movie => movie.Roles)
            .HasForeignKey(role => role.MovieId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>()
            .HasOne<Episode>(role => role.Episode)
            .WithMany(episode => episode.Roles)
            .HasForeignKey(role => role.EpisodeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Episode>()
            .HasOne<Series>(episode => episode.Series)
            .WithMany(series => series.Episodes)
            .HasForeignKey(episode => episode.SeriesId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}