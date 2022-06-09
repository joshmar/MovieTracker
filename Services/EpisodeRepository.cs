using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
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
        await _context.Episodes.Include(episode => episode.Roles).ToListAsync(cancellationToken);

    public async Task<Episode?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Episodes.FindAsync(new object?[] { id }, cancellationToken);

    public async Task<IEnumerable<Episode?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Episodes.Where(episode => ids.Contains(episode.Id)).ToListAsync(cancellationToken);

    public Task<Episode?> CreateAsync(EpisodeModel toCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Guid id, EpisodeModel toUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Episode?> CreateAsync(Episode toCreate, CancellationToken cancellationToken)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Episodes.AddAsync(toCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Episode toUpdate, CancellationToken cancellationToken)
    {
        if (await GetByIdAsync(toUpdate.Id, cancellationToken) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Episodes.Update(toUpdate);
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

    private static bool IsValid(Episode episode) =>
        episode.SeriesId == episode.Series.Id && !episode.Title.IsNullOrWhiteSpace();
}