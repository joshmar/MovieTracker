using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IEpisodeService : ICrudMethods<Episode>
{
    public Task<IEnumerable<Episode>> GetEpisodeBySeriesIdAsync(Guid id, CancellationToken cancellationToken = default);
}