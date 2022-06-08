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
    
    public async Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Movies.ToListAsync(cancellationToken);

    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Movies.FindAsync(id, cancellationToken);
    
    public async IAsyncEnumerable<Movie?> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        foreach (var id in ids)
        {
            yield return await GetByIdAsync(id, cancellationToken);
        }
    }

    public async Task<Movie?> CreateAsync(Movie toCreate, CancellationToken cancellationToken)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Movies.AddAsync(toCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Movie toUpdate, CancellationToken cancellationToken)
    {
        if (await GetByIdAsync(toUpdate.Id, cancellationToken) == null || !IsValid(toUpdate))
            return false;

        _context.Movies.Update(toUpdate);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetByIdAsync(id, cancellationToken);
        if (toDelete == null)
            return false;

        _context.Movies.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static bool IsValid(Movie movie) => 
        !movie.Title.IsNullOrWhiteSpace();
}