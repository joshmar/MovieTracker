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
        await _context.Actors.Include(actor => actor.Roles).ToListAsync(cancellationToken);

    public async Task<Actor?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
        await _context.Actors.FindAsync(new object?[] { id }, cancellationToken);
    
    public async Task<IEnumerable<Actor?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Actors.Where(actor => ids.Contains(actor.Id)).Include(x => x.Roles).ToListAsync(cancellationToken);

    public async Task<Actor?> CreateAsync(ActorModel toCreate, CancellationToken cancellationToken)
    {
        var newActor = new Actor(toCreate.FirstName, toCreate.LastName, toCreate.Score);
        if (!IsValid(newActor))
        {
            return null;
        }

        await _context.Actors.AddAsync(newActor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return newActor;
    }

    public async Task<bool> UpdateAsync(Guid id, ActorModel toUpdate, CancellationToken cancellationToken)
    {
        var toUpdateEntity = await GetByIdAsync(id, cancellationToken);
        if (toUpdateEntity == null || !IsValid(toUpdateEntity)) 
            return false;

        toUpdateEntity.Update(toUpdate);
        
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

    public async Task<bool> AddRoleByRoleIdAsync(Guid actorId, Guid roleId, CancellationToken cancellationToken)
    {
        var roleToAdd = await _roleRepository.GetByIdAsync(roleId, cancellationToken);

        if (roleToAdd == null)
            return false;

        var actorToUpdate = await GetByIdAsync(actorId, cancellationToken);

        if (actorToUpdate == null)
            return false;
        
        actorToUpdate.Roles?.Add(roleToAdd);
        roleToAdd.Actor = actorToUpdate;
        roleToAdd.ActorId = actorId;

        _context.Update(actorToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static bool IsValid(Actor actor) =>
        !actor.FirstName.IsNullOrWhiteSpace() && !actor.LastName.IsNullOrWhiteSpace();
}