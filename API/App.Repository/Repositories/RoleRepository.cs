using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IRoleRepository
    {
        void Add(Role role);
        IQueryable<Role> GetAll();
        IQueryable<Role> GetAllWithClaims();
        IQueryable<Role> GetByIdWithClaims(Guid id);
        void Remove(Guid id);
        void Update(Role role);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly IApplicationDbContext _dbContext;

        private DbSet<Role> DbSet { get => _dbContext.Roles; }
        public RoleRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Role> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<Role> GetAllWithClaims()
        {
            return DbSet.AsNoTracking().Include(x => x.Claims);
        }

        public IQueryable<Role> GetByIdWithClaims(Guid id)
        {
            return DbSet.AsNoTracking().Include(x => x.Claims).Where(x => x.Id == id);
        }

        public void Add(Role role)
        {
            DbSet.Add(role);
        }

        public void Remove(Guid id)
        {
            var objectToDelete = DbSet.Include(x => x.Claims).SingleOrDefault(entity => entity.Id == id);
            if (objectToDelete != null)
            {
                _dbContext.RoleClaims.RemoveRange(objectToDelete.Claims);
                DbSet.Remove(objectToDelete);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }

        public void Update(Role role)
        {
            var itemExistsInDatabase = DbSet.Any(x => x.Id == role.Id);
            if (itemExistsInDatabase)
            {
                var roleClaims = _dbContext.RoleClaims.Include(x => x.Role).Where(x => x.Role.Id == role.Id);
                _dbContext.RoleClaims.RemoveRange(roleClaims);
                _dbContext.SaveChanges();
                _dbContext.RoleClaims.AddRange(role.Claims);
                DbSet.Update(role);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }

    }
}
