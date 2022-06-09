using MovieTracker.Models;
using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IRoleRepository : ICrudMethods<Role, RoleModel>
{
    public Task<IEnumerable<Role>> AddActorRelation(Guid actorId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Role>> AddEpisodeRelation(Guid episodeId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Role>> AddMovieRelation(Guid movieId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Role>> AddSeriesRelation(Guid seriesId, CancellationToken cancellationToken = default);
}