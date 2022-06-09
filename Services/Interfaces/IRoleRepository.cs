using MovieTracker.Models;
using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IRoleRepository : ICrudMethods<Role, RoleModel>
{
    public Task<Role?> AddActorRelation(Guid id, Guid actorId, CancellationToken cancellationToken = default);
    public Task<Role?> AddEpisodeRelation(Guid id, Guid episodeId, CancellationToken cancellationToken = default);
    public Task<Role?> AddMovieRelation(Guid id, Guid movieId, CancellationToken cancellationToken = default);
    public Task<Role?> AddSeriesRelation(Guid id, Guid seriesId, CancellationToken cancellationToken = default);
}