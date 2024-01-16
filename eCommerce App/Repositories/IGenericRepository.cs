using eCommerce_App.Models;

namespace eCommerce_App.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task <IEnumerable<T>> GetAllAsync();
         

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);


    }
}
