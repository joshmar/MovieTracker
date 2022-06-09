using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class SeriesRepository : ISeriesRepository
{
    private readonly MovieTrackerContext _context;

    public SeriesRepository(MovieTrackerContext context)
    {
        _context = context;
    }
    
    public async Task<List<Series>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Series.ToListAsync(cancellationToken);

    public async Task<Series?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Series.FindAsync(new object?[] { id }, cancellationToken);

    public async Task<IEnumerable<Series?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Series.Where(series => ids.Contains(series.Id)).ToListAsync(cancellationToken);

    public Task<Series?> CreateAsync(SeriesModel toCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Guid id, SeriesModel toUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Series?> CreateAsync(Series toCreate, CancellationToken cancellationToken)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Series.AddAsync(toCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Series toUpdate, CancellationToken cancellationToken)
    {
        if (await GetByIdAsync(toUpdate.Id, cancellationToken) == null || !IsValid(toUpdate))
            return false;

        toUpdate.UpdateEpisodeWatched();
        
        _context.Series.Update(toUpdate);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetByIdAsync(id, cancellationToken);
        if (toDelete == null)
            return false;

        _context.Series.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    private static bool IsValid(Series series) =>
        !series.Title.IsNullOrWhiteSpace();
}