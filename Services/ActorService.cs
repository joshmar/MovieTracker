using System.Runtime.CompilerServices;
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

    public async Task<List<Actor>> GetAllAsync(CancellationToken cancellationToken) => 
        await _context.Actors.ToListAsync(cancellationToken);

    public async Task<Actor?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Actors.FindAsync(new object?[] { id }, cancellationToken);
    
    public async IAsyncEnumerable<Actor?> GetByIdsAsync(IEnumerable<Guid> ids, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var id in ids)
        {
            yield return await GetByIdAsync(id, cancellationToken);
        }
    }

    public async Task<Actor?> CreateAsync(Actor toCreate, CancellationToken cancellationToken)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Actors.AddAsync(toCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Actor toUpdate, CancellationToken cancellationToken)
    {
        if (await GetByIdAsync(toUpdate.Id, cancellationToken) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Actors.Update(toUpdate);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetByIdAsync(id, cancellationToken);
        if (toDelete == null)
            return false;

        _context.Actors.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static bool IsValid(Actor actor) =>
        !actor.FirstName.IsNullOrWhiteSpace() && !actor.LastName.IsNullOrWhiteSpace();
}