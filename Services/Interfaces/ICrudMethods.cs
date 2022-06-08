namespace MovieTracker.Services.Interfaces;

public interface ICrudMethods<T>
{
    public Task<List<T>> GetAllAsync();
    
    public Task<List<T?>> GetByIdsAsync(IEnumerable<Guid> id);
    
    public Task<T?> GetByIdAsync(Guid id);
    
    public Task<T?> CreateAsync(T toCreate);
    
    public Task<bool> UpdateAsync(T toUpdate);
    
    public Task<bool> DeleteAsync(Guid id);
}