using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class ActorService : IActorService
{
    private readonly MovieTrackerContext _context;

    public ActorService(MovieTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Actor>> GetAllAsync() => 
        await _context.Actors.ToListAsync();

    public Task<List<Actor?>> GetByIdsAsync(IEnumerable<Guid> id)
    {
        throw new NotImplementedException();
    }

    public async Task<Actor?> GetByIdAsync(Guid id) => 
        await _context.Actors.FindAsync(id);

    public async Task<Actor?> CreateAsync(Actor toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Actors.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Actor toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Actors.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var toDelete = await GetByIdAsync(id);
        if (toDelete == null)
            return false;

        _context.Actors.Remove(toDelete);
        await _context.SaveChangesAsync();

        return true;
    }

    private static bool IsValid(Actor actor) =>
        !actor.FirstName.IsNullOrWhiteSpace() && !actor.LastName.IsNullOrWhiteSpace();
}