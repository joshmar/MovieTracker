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

    public List<Actor> GetAll() => 
        _context.Actors.ToList();

    public async Task<Actor?> GetByIdAsync(Guid id) => 
        await _context.Actors.FindAsync(id);

    public Actor? GetById(Guid id) => 
        _context.Actors.Find(id);

    public async Task<Actor?> CreateAsync(Actor toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Actors.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(toCreate.Id);
    }

    public Actor? Create(Actor toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        _context.Actors.Add(toCreate);
        _context.SaveChanges();
        
        return GetById(toCreate.Id);
    }

    public async Task<bool> UpdateAsync(Actor toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null) 
            return false;
        
        _context.Actors.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool Update(Actor toUpdate)
    {
        if (GetById(toUpdate.Id) == null) 
            return false;
        
        _context.Actors.Update(toUpdate);
        _context.SaveChanges();
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

    public bool Delete(Guid id)
    {
        var toDelete = GetById(id);
        if (toDelete == null)
            return false;

        _context.Actors.Remove(toDelete);
        _context.SaveChanges();

        return true;
    }

    private static bool IsValid(Actor actor) =>
        !actor.FirstName.IsNullOrWhiteSpace() || !actor.LastName.IsNullOrWhiteSpace();
}