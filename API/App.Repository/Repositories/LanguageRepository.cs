using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ILanguageRepository{
        IQueryable<Language> GetAll();
        IQueryable<Language> GetById(Guid id);
    }
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly DbSet<Language> _dbSet;

        public LanguageRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Languages;
        }

        public IQueryable<Language> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<Language> GetById(Guid id)
        {
            return _dbSet.Where(x => x.Id == id);
        }
    }
}
