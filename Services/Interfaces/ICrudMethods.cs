namespace MovieTracker.Services.Interfaces;

public interface ICrudMethods<T, TModel>
{
    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<T?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    
    public Task<T?> CreateAsync(TModel createModel, CancellationToken cancellationToken = default);
    
    public Task<bool> UpdateAsync(Guid id, TModel updateModel, CancellationToken cancellationToken = default);
    
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}