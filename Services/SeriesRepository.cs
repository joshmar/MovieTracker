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
        await _context.Series
            .Include(series => series.Roles)
            .Include(series => series.Episodes)
            .ToListAsync(cancellationToken);

    public async Task<Series?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Series
            .Include(series => series.Roles)
            .Include(series => series.Episodes)
            .Where(series => series.Id == id )
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Series?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Series
            .Include(series => series.Roles)
            .Include(series => series.Episodes)
            .Where(series => ids.Contains(series.Id))
            .ToListAsync(cancellationToken);

    public async Task<Series?> CreateAsync(SeriesModel createModel, CancellationToken cancellationToken = default)
    {
        if (!IsValid(createModel))
            return null;

        var series = new Series(createModel.Title, createModel.Watched, createModel.Description, createModel.Score);
        
        await _context.Series.AddAsync(series, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return series;
    }

    public async Task<bool> UpdateAsync(Guid id, SeriesModel updateModel, CancellationToken cancellationToken = default)
    {
        var toUpdate = await GetByIdAsync(id, cancellationToken);
        if ( toUpdate == null || !IsValid(updateModel))
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

        _context.Series.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    private static bool IsValid(SeriesModel seriesModel) =>
        !seriesModel.Title.IsNullOrWhiteSpace();
}