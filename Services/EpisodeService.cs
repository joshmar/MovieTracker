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

    public List<Episode> GetAll() =>
        _context.Episodes.ToList();

    public async Task<Episode?> GetByIdAsync(Guid id) =>
        await _context.Episodes.FindAsync(id);

    public Episode? GetById(Guid id) =>
        _context.Episodes.Find();

    public async Task<Episode?> CreateAsync(Episode toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Episodes.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(toCreate.Id);
    }

    public Episode? Create(Episode toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        _context.Episodes.Add(toCreate);
        _context.SaveChanges();
        
        return GetById(toCreate.Id);
    }

    public async Task<bool> UpdateAsync(Episode toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Episodes.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool Update(Episode toUpdate)
    {
        if (GetById(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Episodes.Update(toUpdate);
        _context.SaveChanges();
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

    public bool Delete(Guid id)
    {
        var toDelete = GetById(id);
        if (toDelete == null)
            return false;

        _context.Episodes.Remove(toDelete);
        _context.SaveChanges();

        return true;
    }

    private static bool IsValid(Episode toCreate) =>
        toCreate.SeriesId == toCreate.Series.Id && !toCreate.Title.IsNullOrWhiteSpace();
}