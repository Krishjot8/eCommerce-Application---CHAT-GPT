using eCommerce_App.DTOs;
using eCommerce_App.ExceptionMiddleware;
using eCommerce_App.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eCommerce_App.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly List<T> _entities;

        public GenericRepository(List<T> entities)
        {
            _entities = entities;
        }

       

        public Task AddAsync(T entity)
        {
            _entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
          _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> allEntities = _entities.ToList();
            return Task.FromResult(allEntities);
        }

        public Task<T> GetByIdAsync(int id)
        {
            T entity = _entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {

               throw new NotFoundException("The Product you are looking for doesn't exist");
            }
            
            return Task.FromResult(entity);
        }

        public Task UpdateAsync(T updatedEntity)
        {
            var existingEntity = _entities.FirstOrDefault(e => e.Id == updatedEntity.Id);


            if (existingEntity != null) {


                UpdateEntityProperties(existingEntity, updatedEntity);
            }

            return Task.CompletedTask;
        }


        private void UpdateEntityProperties(T existingEntity, T updatedEntity)
            {
            existingEntity.GetType().GetProperty("Name")?.SetValue(existingEntity, updatedEntity.GetType().GetProperty("Name")?.GetValue(updatedEntity));
            existingEntity.GetType().GetProperty("Price")?.SetValue(existingEntity, updatedEntity.GetType().GetProperty("Price")?.GetValue(updatedEntity));
            existingEntity.GetType().GetProperty("Description")?.SetValue(existingEntity, updatedEntity.GetType().GetProperty("Description")?.GetValue(updatedEntity));
            existingEntity.GetType().GetProperty("PictureUrl")?.SetValue(existingEntity, updatedEntity.GetType().GetProperty("PictureUrl")?.GetValue(updatedEntity));
            existingEntity.GetType().GetProperty("ProductTypeId")?.SetValue(existingEntity, updatedEntity.GetType().GetProperty("ProductTypeId")?.GetValue(updatedEntity));
            existingEntity.GetType().GetProperty("ProductBrandId")?.SetValue(existingEntity, updatedEntity.GetType().GetProperty("ProductBrandId")?.GetValue(updatedEntity));

        }
    }
}
