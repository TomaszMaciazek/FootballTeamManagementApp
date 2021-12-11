using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repository.Repositories
{
    public interface IUserRepository
    {
        void Add(User newUser);
        IQueryable<User> GetAll();
        Task<User> GetByEmailOrUsername(string searchString);
        IQueryable<User> GetById(Guid id);
        void Remove(Guid id);
        void Update(User user);
    }

    public class UserRepository : IUserRepository
    {
        protected readonly IApplicationDbContext _dbContext;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Users;
        }

        public IQueryable<User> GetAll()
        {
            return _dbSet.Include(x => x.Role).AsNoTracking();
        }

        public async Task<User> GetByEmailOrUsername(string searchString) => await _dbSet
            .AsNoTracking()
            .Include(x => x.Role).ThenInclude(x => x.Claims)
            .SingleOrDefaultAsync(x => x.Email == searchString || x.Username == searchString);

        public void Add(User newUser)
        {
            _dbSet.Add(newUser);
        }

        public IQueryable<User> GetById(Guid id)
        {
            return _dbSet.Where(x => x.Id == id);
        }

        public void Remove(Guid id)
        {
            var userToDelete = _dbSet.SingleOrDefault(entity => entity.Id == id);
            if (userToDelete != null)
            {
                _dbSet.Remove(userToDelete);
            }
            else
            {
                throw new InvalidOperationException("There is no user with designated Id");
            }
        }

        public void Update(User user)
        {
            _dbSet.Update(user);
        }
    }
}
