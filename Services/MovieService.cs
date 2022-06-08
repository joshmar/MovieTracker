using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class MovieService : IMovieService
{
    private readonly MovieTrackerContext _context;

    public MovieService(MovieTrackerContext context)
    {
        _context = context;
    }
    
    public async Task<List<Movie>> GetAllAsync() =>
        await _context.Movies.ToListAsync();

    public List<Movie> GetAll() =>
        _context.Movies.ToList();

    public async Task<Movie?> GetByIdAsync(Guid id) =>
        await _context.Movies.FindAsync(id);

    public Movie? GetById(Guid id) =>
        _context.Movies.Find(id);

    public async Task<Movie?> CreateAsync(Movie toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Movies.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(toCreate.Id);
    }

    public Movie? Create(Movie toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        _context.Movies.Add(toCreate);
        _context.SaveChanges();
        
        return GetById(toCreate.Id);
    }

    public async Task<bool> UpdateAsync(Movie toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate))
            return false;

        _context.Movies.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool Update(Movie toUpdate)
    {
        if (GetById(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Movies.Update(toUpdate);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var toDelete = await GetByIdAsync(id);
        if (toDelete == null)
            return false;

        _context.Movies.Remove(toDelete);
        await _context.SaveChangesAsync();

        return true;
    }

    public bool Delete(Guid id)
    {
        var toDelete = GetById(id);
        if (toDelete == null)
            return false;

        _context.Movies.Remove(toDelete);
        _context.SaveChanges();

        return true;
    }

    private static bool IsValid(Movie toCreate) => 
        !toCreate.Title.IsNullOrWhiteSpace();
}