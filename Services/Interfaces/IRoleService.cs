using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IRoleService : ICrudMethods<Role>
{
    public Task<IEnumerable<Role>> GetByActorIdAsync(Guid actorId);
    public Task<IEnumerable<Role>> GetByEpisodeIdAsync(Guid episodeId);
    public Task<IEnumerable<Role>> GetByMovieIdAsync(Guid movieId);
    public Task<IEnumerable<Role>> GetBySeriesIdAsync(Guid seriesId);
}