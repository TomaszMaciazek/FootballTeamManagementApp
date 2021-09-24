using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Common
{
    public interface IService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> DeactivateAsync(Guid id);
        Task<bool> ActivateAsync(Guid id);
    }
}
