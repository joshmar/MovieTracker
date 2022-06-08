using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class EpisodeService : IEpisodeService
{
    public Task<List<Episode>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public List<Episode> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Episode?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Episode? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Episode?> CreateAsync(Episode toCreate)
    {
        throw new NotImplementedException();
    }

    public Episode? Create(Episode toCreate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Episode toUpdate)
    {
        throw new NotImplementedException();
    }

    public bool Update(Episode toUpdate)
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