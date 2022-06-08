using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IRoleService : ICrudMethods<Role>
{
    public Task<List<Role>> GetByActorIdAsync(Guid actorId);
    public Task<List<Role>> GetByEpisodeIdAsync(Guid episodeId);
    public Task<List<Role>> GetByMovieIdAsync(Guid movieId);
    public Task<List<Role>> GetBySeriesIdAsync(Guid seriesId);
}