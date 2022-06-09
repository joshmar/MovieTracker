using MovieTracker.Models;
using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IEpisodeRepository : ICrudMethods<Episode, EpisodeModel>
{
    public Task<IEnumerable<Episode>> GetBySeriesIdAsync(Guid id, CancellationToken cancellationToken = default);
}