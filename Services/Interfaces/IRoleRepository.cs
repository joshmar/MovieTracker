﻿using MovieTracker.Models;
using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IRoleRepository : ICrudMethods<Role, RoleModel>
{
    public Task<IEnumerable<Role>> GetByActorIdAsync(Guid actorId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Role>> GetByEpisodeIdAsync(Guid episodeId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Role>> GetByMovieIdAsync(Guid movieId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Role>> GetBySeriesIdAsync(Guid seriesId, CancellationToken cancellationToken = default);
}