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
    public interface IMatchPlayerPerformanceService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(MatchPlayerPerformance entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<MatchPlayerPerformance>> GetAllAsync();
        Task<MatchPlayerPerformance> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(MatchPlayerPerformance entity);
    }

    public class MatchPlayerPerformanceService : IService<MatchPlayerPerformance>, IMatchPlayerPerformanceService
    {
        private readonly IMatchPlayerPerformanceRepository _matchPlayerRepository;
        private readonly IApplicationDbContext _context;

        public MatchPlayerPerformanceService(IMatchPlayerPerformanceRepository matchPlayerRepository, IApplicationDbContext context)
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

        public async Task AddAsync(MatchPlayerPerformance entity)
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

        public async Task<List<MatchPlayerPerformance>> GetAllAsync() => await _matchPlayerRepository.GetAll().ToListAsync();

        public async Task<MatchPlayerPerformance> GetByIdAsync(Guid id) => await _matchPlayerRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _matchPlayerRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MatchPlayerPerformance entity)
        {
            _matchPlayerRepository.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
