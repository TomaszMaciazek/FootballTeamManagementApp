using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Common.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetById(Guid id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(Guid id);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
