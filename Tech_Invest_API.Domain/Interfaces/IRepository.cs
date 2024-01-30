using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<IList<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
