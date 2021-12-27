using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ICoachCardService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(CoachCard entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<CoachCard>> GetAllAsync();
        Task<CoachCard> GetByIdAsync(Guid id);
        Task<List<CoachCard>> GetCardsFromCoachAsync(Guid coachId);
        Task<List<CoachCard>> GetCoachesCardsFromMatchAsync(Guid matchId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(CoachCard entity);
    }

    public class CoachCardService : IService<CoachCard>, ICoachCardService
    {
        private readonly ICoachCardRepository _cardRepository;
        private readonly IApplicationDbContext _context;

        public CoachCardService(ICoachCardRepository cardRepository, IApplicationDbContext context)
        {
            _cardRepository = cardRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _cardRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(CoachCard entity)
        {
            _cardRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _cardRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CoachCard>> GetAllAsync() => await _cardRepository.GetAll().AsNoTracking().ToListAsync();


        public async Task<CoachCard> GetByIdAsync(Guid id) => await _cardRepository.GetById(id).AsNoTracking().SingleOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _cardRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CoachCard entity)
        {
            _cardRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CoachCard>> GetCardsFromCoachAsync(Guid coachId)
            => await _cardRepository.GetAllEager().Where(x => x.Coach.Id == coachId && x.IsActive).ToListAsync();


        public async Task<List<CoachCard>> GetCoachesCardsFromMatchAsync(Guid matchId)
            => await _cardRepository.GetAllEager().Where(x => x.Match.Id == matchId && x.IsActive).ToListAsync();
    }
}
