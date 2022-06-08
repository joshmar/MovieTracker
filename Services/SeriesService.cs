using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class SeriesService : ISeriesService
{
    private readonly MovieTrackerContext _context;

    public SeriesService(MovieTrackerContext context)
    {
        _context = context;
    }
    
    public async Task<List<Series>> GetAllAsync() =>
        await _context.Series.ToListAsync();

    public async Task<Series?> GetByIdAsync(Guid id) =>
        await _context.Series.FindAsync(id);

    public async IAsyncEnumerable<Series?> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            yield return await GetByIdAsync(id);
        }
    }
    
    public async Task<Series?> CreateAsync(Series toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Series.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Series toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate))
            return false;

        toUpdate.UpdateEpisodeWatched();
        
        _context.Series.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var toDelete = await GetByIdAsync(id);
        if (toDelete == null)
            return false;

        _context.Series.Remove(toDelete);
        await _context.SaveChangesAsync();

        return true;
    }
    
    private static bool IsValid(Series series) =>
        !series.Title.IsNullOrWhiteSpace();
}