using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IMatchPlayerScoreService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(MatchPlayerScore entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<MatchPlayerScore>> GetAllAsync();
        Task<MatchPlayerScore> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateMatchPlayerScoreVM command);
    }

    public class MatchPlayerScoreService : IMatchPlayerScoreService
    {
        private readonly IMatchPlayerScoreRepository _matchPlayerScoreRepository;
        private readonly IApplicationDbContext _context;

        public MatchPlayerScoreService(IMatchPlayerScoreRepository matchPlayerScoreRepository, IApplicationDbContext context)
        {
            _matchPlayerScoreRepository = matchPlayerScoreRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _matchPlayerScoreRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(MatchPlayerScore entity)
        {
            _matchPlayerScoreRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _matchPlayerScoreRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MatchPlayerScore>> GetAllAsync() => await _matchPlayerScoreRepository.GetAll().AsNoTracking().ToListAsync();

        public async Task<MatchPlayerScore> GetByIdAsync(Guid id) => await _matchPlayerScoreRepository.GetById(id).AsNoTracking().FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _matchPlayerScoreRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateMatchPlayerScoreVM command)
        {
            var score = await _matchPlayerScoreRepository.GetById(command.Id).SingleOrDefaultAsync();

            score.Value = command.Value;

            _matchPlayerScoreRepository.Update(score);
            await _context.SaveChangesAsync();
        }
    }
}
