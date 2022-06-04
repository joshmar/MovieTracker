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
        //Many to many role - actor mapping
        modelBuilder.Entity<RoleActor>()
            .HasKey(roleActor => new { roleActor.ActorId, roleActor.RoleId });

        modelBuilder.Entity<RoleActor>()
            .HasOne(roleActor => roleActor.Actor)
            .WithMany(actor => actor.RoleActors)
            .HasForeignKey(roleActor => roleActor.ActorId);
        
        modelBuilder.Entity<RoleActor>()
            .HasOne(roleActor => roleActor.Role)
            .WithMany(role => role.RoleActors)
            .HasForeignKey(roleActor => roleActor.RoleId);
        
        //Many to many role - episode mapping
        modelBuilder.Entity<RoleEpisode>()
            .HasKey(roleEpisode => new { roleEpisode.EpisodeId, roleEpisode.RoleId });

        modelBuilder.Entity<RoleEpisode>()
            .HasOne(roleEpisode => roleEpisode.Episode)
            .WithMany(episode => episode.RoleEpisodes)
            .HasForeignKey(roleEpisode => roleEpisode.EpisodeId);
        
        modelBuilder.Entity<RoleEpisode>()
            .HasOne(roleEpisode => roleEpisode.Role)
            .WithMany(role => role.RoleEpisodes)
            .HasForeignKey(roleEpisode => roleEpisode.RoleId);
        
        //Many to many role - movie mapping
        modelBuilder.Entity<RoleMovie>()
            .HasKey(roleMovie => new { roleMovie.MovieId, roleMovie.RoleId });

        modelBuilder.Entity<RoleMovie>()
            .HasOne(roleMovie => roleMovie.Movie)
            .WithMany(movie => movie.RoleMovies)
            .HasForeignKey(roleMovie => roleMovie.MovieId);
        
        modelBuilder.Entity<RoleMovie>()
            .HasOne(roleMovie => roleMovie.Role)
            .WithMany(role => role.RoleMovies)
            .HasForeignKey(roleMovie => roleMovie.RoleId);
        
        //Many to many role - series mapping
        modelBuilder.Entity<RoleSeries>()
            .HasKey(roleSeries => new { roleSeries.SeriesId, roleSeries.RoleId });

        modelBuilder.Entity<RoleSeries>()
            .HasOne(roleSeries => roleSeries.Series)
            .WithMany(series => series.RoleSeries)
            .HasForeignKey(roleSeries => roleSeries.SeriesId);
        
        modelBuilder.Entity<RoleSeries>()
            .HasOne(roleSeries => roleSeries.Role)
            .WithMany(role => role.RoleSeries)
            .HasForeignKey(roleSeries => roleSeries.RoleId);
        
        //one to Many series - episode mapping
        modelBuilder.Entity<SeriesEpisode>()
            .HasKey(seriesEpisode => new { seriesEpisode.SeriesId, seriesEpisode.EpisodeId });

        modelBuilder.Entity<SeriesEpisode>()
            .HasOne(seriesEpisode => seriesEpisode.Series)
            .WithMany(series => series.SeriesEpisodes)
            .HasForeignKey(seriesEpisode => seriesEpisode.SeriesId);
        
        modelBuilder.Entity<SeriesEpisode>()
            .HasOne(seriesEpisode => seriesEpisode.Episode)
            .WithOne(episode => episode.SeriesEpisode)
            .HasForeignKey<SeriesEpisode>(episode => episode.EpisodeId);
    }
}