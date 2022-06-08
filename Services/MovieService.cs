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

    public Task<List<Movie?>> GetByIdsAsync(IEnumerable<Guid> id)
    {
        throw new NotImplementedException();
    }

    public async Task<Movie?> GetByIdAsync(Guid id) =>
        await _context.Movies.FindAsync(id);

    public async Task<Movie?> CreateAsync(Movie toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Movies.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Movie toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate))
            return false;

        _context.Movies.Update(toUpdate);
        await _context.SaveChangesAsync();
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

    private static bool IsValid(Movie movie) => 
        !movie.Title.IsNullOrWhiteSpace();
}