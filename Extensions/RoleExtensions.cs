using MovieTracker.Models.Entities;

namespace MovieTracker.Extension;

public static class RoleExtensions
{
    public static bool AppearsInMovie(this Role role) =>
        role.Movie != null && role.MovieId != null && role.Movie?.Id == role.MovieId;

    public static bool AppearsInEpisodeAndSeries(this Role role) =>
        role.AppearsInSeries() && role.AppearsInEpisode();
    
    public static bool AppearsInSeries(this Role role) =>
        role.Series != null && role.SeriesId != null && role.Series?.Id == role.SeriesId;
    
    public static bool AppearsInEpisode(this Role role) =>
        role.Episode != null && role.EpisodeId != null && role.Episode?.Id == role.EpisodeId;

    public static bool HasAppearance(this Role role) =>
        role.AppearsInMovie() || role.AppearsInSeries() || role.AppearsInEpisode();
}