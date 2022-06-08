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

    public List<Series> GetAll() =>
        _context.Series.ToList();

    public async Task<Series?> GetByIdAsync(Guid id) =>
        await _context.Series.FindAsync(id);

    public Series? GetById(Guid id) =>
        _context.Series.Find(id);

    public async Task<Series?> CreateAsync(Series toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Series.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(toCreate.Id);
    }

    public Series? Create(Series toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        _context.Series.Add(toCreate);
        _context.SaveChanges();
        
        return GetById(toCreate.Id);
    }

    public async Task<bool> UpdateAsync(Series toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate))
            return false;

        _context.Series.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool Update(Series toUpdate)
    {
        if (GetById(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Series.Update(toUpdate);
        _context.SaveChanges();
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

    public bool Delete(Guid id)
    {
        var toDelete = GetById(id);
        if (toDelete == null)
            return false;

        _context.Series.Remove(toDelete);
        _context.SaveChanges();

        return true;
    }
    
    private static bool IsValid(Series series) =>
        !series.Title.IsNullOrWhiteSpace();
}