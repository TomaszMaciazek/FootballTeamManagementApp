using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.Enums;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IRoleService
    {
        Task AddAsync(Role role);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<Role>> GetAllWithClaimsAsync();
        Task<Role> GetByIdAsync(Guid id);
        Task<Role> GetRole(UserRole role);
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

        public async Task<Role> GetByIdAsync(Guid id) => await _roleRepository.GetByIdWithClaims(id).FirstOrDefaultAsync();

        public async Task<Role> GetRole(UserRole role) => role switch
        {
            UserRole.Administrator => await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == "admin"),
            UserRole.Coach => await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == "coach"),
            UserRole.Player => await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == "player"),
            _ => null
        };

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
