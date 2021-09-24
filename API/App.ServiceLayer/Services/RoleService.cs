using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IRoleService
    {
        Task AddAsync(Role role);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<Role>> GetAllWithClaimsAsync();
        Task<IEnumerable<Role>> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Role role);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IApplicationDbContext _context;

        public RoleService(IRoleRepository roleRepository, IApplicationDbContext context)
        {
            _roleRepository = roleRepository;
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            _roleRepository.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync() => await _roleRepository.GetAll().ToListAsync();

        public async Task<IEnumerable<Role>> GetAllWithClaimsAsync() => await _roleRepository.GetAllWithClaims().ToListAsync();

        public async Task<IEnumerable<Role>> GetByIdAsync(Guid id) => await _roleRepository.GetByIdWithClaims(id).ToListAsync();

        public async Task UpdateAsync(Role role)
        {
            _roleRepository.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            _roleRepository.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}
