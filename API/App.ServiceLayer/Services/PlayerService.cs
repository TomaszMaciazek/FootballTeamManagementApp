using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IPlayerService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Player entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(Guid id);
        Task<Player> GetByIdEagerAsync(Guid id);
        Task<Player> GetByUserIdEagerAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Player entity);
    }

    public class PlayerService : IService<Player>, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IApplicationDbContext _context;

        public PlayerService(IPlayerRepository playerRepository, IApplicationDbContext context)
        {
            _playerRepository = playerRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _playerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Player entity)
        {
            _playerRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _playerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Player>> GetAllAsync() => await _playerRepository.GetAll().ToListAsync();

        public async Task<Player> GetByIdAsync(Guid id) => await _playerRepository.GetById(id).FirstOrDefaultAsync();

        public async Task<Player> GetByIdEagerAsync(Guid id) => await _playerRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task<Player> GetByUserIdEagerAsync(Guid id) => await _playerRepository.GetByUserIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _playerRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Player entity)
        {
            _playerRepository.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
