using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class RoleRepository : IRoleRepository
{
    private readonly MovieTrackerContext _context;

    public RoleRepository(MovieTrackerContext context)
    {
        _context = context;
    }
    
    public async Task<List<Role>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Roles.ToListAsync(cancellationToken);

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Roles.FindAsync(new object?[] { id }, cancellationToken);

    public async Task<IEnumerable<Role?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Roles.Where(role => ids.Contains(role.Id)).ToListAsync(cancellationToken);

    public Task<Role?> CreateAsync(RoleModel toCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Guid id, RoleModel toUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Role?> CreateAsync(Role toCreate, CancellationToken cancellationToken)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Roles.AddAsync(toCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return await GetByIdAsync(toCreate.Id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Role toUpdate, CancellationToken cancellationToken)
    {
        if (await GetByIdAsync(toUpdate.Id, cancellationToken) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Roles.Update(toUpdate);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetByIdAsync(id, cancellationToken);
        if (toDelete == null)
            return false;

        _context.Roles.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<IEnumerable<Role>> GetByActorIdAsync(Guid actorId, CancellationToken cancellationToken) => 
        await _context.Roles.Where(role => role.ActorId == actorId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Role>> GetByEpisodeIdAsync(Guid episodeId, CancellationToken cancellationToken) => 
        await _context.Roles.Where(role => role.EpisodeId == episodeId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Role>> GetByMovieIdAsync(Guid movieId, CancellationToken cancellationToken) => 
        await _context.Roles.Where(role => role.MovieId == movieId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Role>> GetBySeriesIdAsync(Guid seriesId, CancellationToken cancellationToken) => 
        await _context.Roles.Where(role => role.SeriesId == seriesId).ToListAsync(cancellationToken);

    private static bool IsValid(Role role) => 
        !role.Name.IsNullOrWhiteSpace();
}