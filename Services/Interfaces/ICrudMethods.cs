namespace MovieTracker.Services.Interfaces;

public interface ICrudMethods<T>
{
    public Task<List<T>> GetAllAsync();
    public List<T> GetAll();
    
    public Task<T?> GetByIdAsync(Guid id);
    public T? GetById(Guid id);
    
    public Task<T?> CreateAsync(T toCreate);
    public T? Create(T toCreate);
    
    public Task<bool> UpdateAsync(T toUpdate);
    public bool Update(T toUpdate);
    
    public Task<bool> DeleteAsync(Guid id);
    public bool Delete(Guid id);
}