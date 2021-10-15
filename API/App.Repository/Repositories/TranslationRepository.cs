using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITranslationRepository
    {
        IQueryable<Translation> GetAll();
        IQueryable<Translation> GetById(Guid id);
    }

    public class TranslationRepository : ITranslationRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly DbSet<Translation> _dbSet;

        public TranslationRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Translations;
        }

        public IQueryable<Translation> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<Translation> GetById(Guid id)
        {
            return _dbSet.Where(x => x.Id == id);
        }
    }
}
