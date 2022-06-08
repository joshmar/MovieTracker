using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class MovieService : IMovieService
{
    public Task<List<Movie>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public List<Movie> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Movie? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> CreateAsync(Movie toCreate)
    {
        throw new NotImplementedException();
    }

    public Movie? Create(Movie toCreate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Movie toUpdate)
    {
        throw new NotImplementedException();
    }

    public bool Update(Movie toUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}