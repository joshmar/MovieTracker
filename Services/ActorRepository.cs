using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class ActorRepository : IActorRepository
{
    private readonly MovieTrackerContext _context;
    private readonly IRoleRepository _roleRepository;

    public ActorRepository(MovieTrackerContext context, IRoleRepository roleRepository)
    {
        _context = context;
        _roleRepository = roleRepository;
    }

    public async Task<List<Actor>> GetAllAsync(CancellationToken cancellationToken) => 
        await _context.Actors
            .Include(actor => actor.Roles)
            .ToListAsync(cancellationToken);

    public async Task<Actor?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Actors
            .Include(actor => actor.Roles)
            .Where(actor => actor.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<IEnumerable<Actor?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Actors
            .Include(actor => actor.Roles)
            .Where(actor => ids.Contains(actor.Id))
            .ToListAsync(cancellationToken);

    public async Task<Actor?> CreateAsync(ActorModel createModel, CancellationToken cancellationToken)
    {
        if (!IsValid(createModel))
            return null;

        var actor = new Actor(createModel.FirstName, createModel.LastName, createModel.Score);
        
        await _context.Actors.AddAsync(actor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return actor;
    }

    public async Task<bool> UpdateAsync(Guid id, ActorModel updateModel, CancellationToken cancellationToken)
    {
        var toUpdate = await GetByIdAsync(id, cancellationToken);
        if (toUpdate == null || !IsValid(updateModel)) 
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

        _context.Actors.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static bool IsValid(ActorModel actorModel) =>
        !actorModel.FirstName.IsNullOrWhiteSpace() && !actorModel.LastName.IsNullOrWhiteSpace();
}