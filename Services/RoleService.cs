using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class RoleService : IRoleService
{
    private readonly MovieTrackerContext _context;

    public RoleService(MovieTrackerContext context)
    {
        _context = context;
    }
    
    public async Task<List<Role>> GetAllAsync() =>
        await _context.Roles.ToListAsync();

    public async Task<Role?> GetByIdAsync(Guid id) =>
        await _context.Roles.FindAsync(id);

    public async IAsyncEnumerable<Role?> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            yield return await GetByIdAsync(id);
        }
    }

    public async Task<Role?> CreateAsync(Role toCreate)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Roles.AddAsync(toCreate);
        await _context.SaveChangesAsync();
        
        return await GetByIdAsync(toCreate.Id);
    }

    public async Task<bool> UpdateAsync(Role toUpdate)
    {
        if (await GetByIdAsync(toUpdate.Id) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Roles.Update(toUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var toDelete = await GetByIdAsync(id);
        if (toDelete == null)
            return false;

        _context.Roles.Remove(toDelete);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<List<Role>> GetByActorIdAsync(Guid actorId) => 
        await _context.Roles.Where(role => role.ActorId == actorId).ToListAsync();

    public async Task<List<Role>> GetByEpisodeIdAsync(Guid episodeId) => 
        await _context.Roles.Where(role => role.EpisodeId == episodeId).ToListAsync();

    public async Task<List<Role>> GetByMovieIdAsync(Guid movieId) => 
        await _context.Roles.Where(role => role.MovieId == movieId).ToListAsync();

    public async Task<List<Role>> GetBySeriesIdAsync(Guid seriesId) => 
        await _context.Roles.Where(role => role.SeriesId == seriesId).ToListAsync();

    private static bool IsValid(Role role)
    {
        var actorValidation = role.ActorId != Guid.Empty
                              && role.Actor.Id == role.ActorId;

        if (!role.HasAppearance())
            return actorValidation;

        var episodeAndSeriesValidation = role.AppearsInEpisodeAndSeries() || role.AppearsInSeries();
        
        if (!role.AppearsInMovie())
            return actorValidation && episodeAndSeriesValidation;

        return actorValidation && !episodeAndSeriesValidation && !role.AppearsInEpisode();
    }
}