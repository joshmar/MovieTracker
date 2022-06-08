using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class SeriesService : ISeriesService
{
    public Task<List<Series>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public List<Series> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Series?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Series? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Series?> CreateAsync(Series toCreate)
    {
        throw new NotImplementedException();
    }

    public Series? Create(Series toCreate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Series toUpdate)
    {
        throw new NotImplementedException();
    }

    public bool Update(Series toUpdate)
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