namespace MovieTracker.Services.Interfaces;

public interface ICrudMethods<T>
{
    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    public IAsyncEnumerable<T?> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    
    public Task<T?> CreateAsync(T toCreate, CancellationToken cancellationToken = default);
    
    public Task<bool> UpdateAsync(T toUpdate, CancellationToken cancellationToken = default);
    
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}