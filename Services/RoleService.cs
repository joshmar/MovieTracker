using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class RoleService : IRoleService
{
    public Task<List<Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public List<Role> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Role?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Role? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Role?> CreateAsync(Role toCreate)
    {
        throw new NotImplementedException();
    }

    public Role? Create(Role toCreate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Role toUpdate)
    {
        throw new NotImplementedException();
    }

    public bool Update(Role toUpdate)
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