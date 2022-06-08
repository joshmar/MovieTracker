namespace MovieTracker.Services.Interfaces;

public interface ICrudMethods<T>
{
    public Task<List<T>> GetAllAsync();

    public Task<T?> GetByIdAsync(Guid id);
    
    public IAsyncEnumerable<T?> GetByIdsAsync(IEnumerable<Guid> ids);
    
    public Task<T?> CreateAsync(T toCreate);
    
    public Task<bool> UpdateAsync(T toUpdate);
    
    public Task<bool> DeleteAsync(Guid id);
}