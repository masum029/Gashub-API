
namespace Project.Application.Interfaces
{
    public interface IBaseServices<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(Guid id, T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
