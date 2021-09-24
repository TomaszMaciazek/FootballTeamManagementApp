using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.ServiceLayer.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserService
    {
        Task<bool> Activate(Guid id);
        Task Add(User entity);
        Task<bool> Deactivate(Guid id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmailOrUsername(string searchString);
        Task<User> GetById(Guid id);
        Task Remove(Guid id);
        Task Update(User entity);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationDbContext _context;

        public UserService(IUserRepository userRepository, IApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<bool> Activate(Guid id)
        {
            var userToActivate = await _userRepository.GetById(id).SingleOrDefaultAsync();
            if (userToActivate == null)
            {
                return false;
            }

            userToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Add(User entity)
        {
            _userRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Deactivate(Guid id)
        {
            var objectToDeactivate = await _userRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetByEmailOrUsername(string searchString) => await _userRepository.GetByEmailOrUsername(searchString);

        public async Task<IEnumerable<User>> GetAll() => await _userRepository.GetAll().ToListAsync();

        public async Task<User> GetById(Guid id) => await _userRepository.GetById(id).SingleOrDefaultAsync();

        public async Task Remove(Guid id)
        {
            _userRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _userRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<User>> GetUsers(UserQuery query)
        {
            var users = _userRepository.GetAll().Where(x => x.IsActive);

            users = users.WhereStringPropertyContains(x => x.Email, query.Email);

            users = users.WhereStringPropertyContains(x => x.Name, query.Firstname);

            users = users.WhereStringPropertyContains(x => x.Surname, query.Surname);

            users = users.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await users.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
