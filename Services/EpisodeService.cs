﻿using Microsoft.EntityFrameworkCore;
using MovieTracker.Extension;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.Services;

public class EpisodeService : IEpisodeService
{
    private readonly MovieTrackerContext _context;

    public EpisodeService(MovieTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Episode>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Episodes.ToListAsync(cancellationToken);

    public async Task<Episode?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Episodes.FindAsync(id, cancellationToken);

    public async IAsyncEnumerable<Episode?> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        foreach (var id in ids)
        {
            yield return await GetByIdAsync(id, cancellationToken);
        }
    }
    
    public async Task<Episode?> CreateAsync(Episode toCreate, CancellationToken cancellationToken)
    {
        if (!IsValid(toCreate))
        {
            return null;
        }

        await _context.Episodes.AddAsync(toCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return toCreate;
    }

    public async Task<bool> UpdateAsync(Episode toUpdate, CancellationToken cancellationToken)
    {
        if (await GetByIdAsync(toUpdate.Id, cancellationToken) == null || !IsValid(toUpdate)) 
            return false;
        
        _context.Episodes.Update(toUpdate);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetByIdAsync(id, cancellationToken);
        if (toDelete == null)
            return false;

        _context.Episodes.Remove(toDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Episode>> GetBySeriesIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Episodes.Where(episode => episode.SeriesId == id).ToListAsync(cancellationToken);

    private static bool IsValid(Episode episode) =>
        episode.SeriesId == episode.Series.Id && !episode.Title.IsNullOrWhiteSpace();
}