using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class MovieRepository : IMovieRepository
{
    private readonly MovieTrackerContext _context;

    public MovieRepository(MovieTrackerContext context)
    {
        _context = context;
    }
    
    public async Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Movies
            .Include(movie => movie.Roles)
            .ToListAsync(cancellationToken);

    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Movies
            .Include(movie => movie.Roles)
            .Where(movie => movie.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<IEnumerable<Movie?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Movies
            .Include(movie => movie.Roles)
            .Where(movie => ids.Contains(movie.Id))
            .ToListAsync(cancellationToken);

    public async Task<Movie?> CreateAsync(MovieModel createModel, CancellationToken cancellationToken = default)
    {
        if (!IsValid(createModel))
            return null;

        var movie = new Movie(createModel.Title, createModel.Watched, createModel.Description, createModel.Score);
        
        await _context.Movies.AddAsync(movie, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return movie;
    }

    public async Task<bool> UpdateAsync(Guid id, MovieModel updateModel, CancellationToken cancellationToken = default)
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

        _context.Movies.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static bool IsValid(MovieModel movieModel) => 
        !movieModel.Title.IsNullOrWhiteSpace();
}