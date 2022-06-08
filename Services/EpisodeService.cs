using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class EpisodeService : IEpisodeService
{
    private readonly MovieTrackerContext _context;

    public EpisodeService(MovieTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Episode>> GetAllAsync() =>
        await _context.Episodes.ToListAsync();

    public Task<List<Episode?>> GetByIdsAsync(IEnumerable<Guid> id)
    {
        throw new NotImplementedException();
    }

    public async Task<Episode?> GetByIdAsync(Guid id) =>
        await _context.Episodes.FindAsync(id);

    public async Task<Episode?> CreateAsync(Episode toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Episodes.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Episode toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Episodes.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var toDelete = await GetByIdAsync(id);
        if (toDelete == null)
            return false;

        _context.Episodes.Remove(toDelete);
        await _context.SaveChangesAsync();

        return true;
    }

    private static bool IsValid(Episode episode) =>
        episode.SeriesId == episode.Series.Id && !episode.Title.IsNullOrWhiteSpace();
}