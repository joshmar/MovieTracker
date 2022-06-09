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
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .ToListAsync(cancellationToken);

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .Where( role => role.Id == id )
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Role?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) => 
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .Where(role => ids.Contains(role.Id))
            .ToListAsync(cancellationToken);

    public async Task<Role?> CreateAsync(RoleModel createModel, CancellationToken cancellationToken = default)
    {
        if (!IsValid(createModel))
            return null;

        var role = new Role(name: createModel.Name, actorId: createModel.ActorId,
            seriesId: createModel.SeriesId, episodeId: createModel.EpisodeId,
            movieId: createModel.MovieId, description: createModel.Description,
            score: createModel.Score);

        await _context.Roles.AddAsync(role, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return role;
    }

    public async Task<bool> UpdateAsync(Guid id, RoleModel updateModel, CancellationToken cancellationToken = default)
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

        _context.Roles.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<IEnumerable<Role>> AddActorRelation(Guid actorId, CancellationToken cancellationToken) => 
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .Where(role => role.ActorId == actorId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Role>> AddEpisodeRelation(Guid episodeId, CancellationToken cancellationToken) => 
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .Where(role => role.EpisodeId == episodeId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Role>> AddMovieRelation(Guid movieId, CancellationToken cancellationToken) => 
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .Where(role => role.MovieId == movieId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Role>> AddSeriesRelation(Guid seriesId, CancellationToken cancellationToken) => 
        await _context.Roles
            .Include(role => role.Actor)
            .Include(role => role.Series)
            .Include(role => role.Episode)
            .Include(role => role.Movie)
            .Where(role => role.SeriesId == seriesId)
            .ToListAsync(cancellationToken);

    private static bool IsValid(RoleModel roleModel) => 
        !roleModel.Name.IsNullOrWhiteSpace();
}