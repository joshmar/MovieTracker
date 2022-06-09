using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.GQL.Queries;
using MovieTracker.Models;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class EpisodeRepository : IEpisodeRepository
{
    private readonly MovieTrackerContext _context;

    public EpisodeRepository(MovieTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Episode>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Episodes
            .Include(episode => episode.Roles)
            .Include(episode => episode.Series)
            .ToListAsync(cancellationToken);

    public async Task<Episode?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Episodes
            .Include(episode => episode.Roles)
            .Include(episode => episode.Series)
            .Where(episode => episode.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Episode?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Episodes
            .Include(episode => episode.Roles)
            .Include(episode => episode.Series)
            .Where(episode => ids.Contains(episode.Id))
            .ToListAsync(cancellationToken);

    public async Task<Episode?> CreateAsync(EpisodeModel createModel, CancellationToken cancellationToken = default)
    {
        if (!IsValid(createModel))
            return null;

        var episode = new Episode(createModel.Title, createModel.Watched, createModel.SeriesId, createModel.Description, createModel.Score);
        await _context.Episodes.AddAsync(episode, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return episode;
    }

    public async Task<bool> UpdateAsync(Guid id, EpisodeModel updateModel, CancellationToken cancellationToken = default)
    {
        var toUpdate = await GetByIdAsync(id, cancellationToken);
        if (toUpdate == null || !IsValid(updateModel)) 
            return false;
        
        toUpdate.Update(updateModel);
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetByIdAsync(id, cancellationToken);
        if (toDelete == null)
            return false;

        _context.Episodes.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Episode>> GetBySeriesIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Episodes.Where(episode => episode.SeriesId == id).ToListAsync(cancellationToken);

    private static bool IsValid(EpisodeModel episodeModel) =>
        !episodeModel.Title.IsNullOrWhiteSpace();
}