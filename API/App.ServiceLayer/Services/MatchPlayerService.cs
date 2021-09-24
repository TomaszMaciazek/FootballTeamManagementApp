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
    public interface IMatchPlayerService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(MatchPlayer entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<MatchPlayer>> GetAllAsync();
        Task<MatchPlayer> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(MatchPlayer entity);
    }

    public class MatchPlayerService : IService<MatchPlayer>, IMatchPlayerService
    {
        private readonly IMatchPlayerRepository _matchPlayerRepository;
        private readonly IApplicationDbContext _context;

        public MatchPlayerService(IMatchPlayerRepository matchPlayerRepository, IApplicationDbContext context)
        {
            _matchPlayerRepository = matchPlayerRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _matchPlayerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(MatchPlayer entity)
        {
            _matchPlayerRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _matchPlayerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MatchPlayer>> GetAllAsync() => await _matchPlayerRepository.GetAll().ToListAsync();

        public async Task<MatchPlayer> GetByIdAsync(Guid id) => await _matchPlayerRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _matchPlayerRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MatchPlayer entity)
        {
            _matchPlayerRepository.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
