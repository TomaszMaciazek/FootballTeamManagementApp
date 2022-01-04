using App.DataAccess.Interfaces;
using App.Model.Entities.Common;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Common.Implementation
{
    public abstract class BaseRepository<T> : IRepository<T> where T : EditableEntity
    {
        protected readonly IApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IQueryable<T> GetById(Guid id)
        {
            return _dbSet.Where(x => x.Id == id);
        }

        public void Remove(Guid id)
        {
            var objectToDelete = _dbSet.SingleOrDefault(entity => entity.Id == id);
            if (objectToDelete != null)
            {
                _dbSet.Remove(objectToDelete);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            var itemExistsInDatabase = _dbSet.Any(x => x.Id == entity.Id);
            if (itemExistsInDatabase)
            {
                _dbSet.Update(entity);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }
    }
}
