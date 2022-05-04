using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAll();
        IQueryable<Country> GetById(int id);
    }

    public class CountryRepository : ICountryRepository
    {

        private readonly IApplicationDbContext _dbContext;

        private DbSet<Country> DbSet { get => _dbContext.Countries; }
        public CountryRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Country> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<Country> GetById(int id)
        {
            return DbSet.AsNoTracking().Where(x => x.Id == id);
        }
    }
}
