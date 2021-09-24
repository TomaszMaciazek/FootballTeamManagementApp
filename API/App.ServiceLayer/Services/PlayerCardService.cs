using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IPlayerCardService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(PlayerCard entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<PlayerCard>> GetAllAsync();
        Task<PlayerCard> GetByIdAsync(Guid id);
        Task<List<PlayerCard>> GetCardsFromPlayerAsync(Guid playerId);
        Task<List<PlayerCard>> GetPlayersCardsFromMatchAsync(Guid matchId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(PlayerCard entity);
    }

    public class PlayerCardService : IService<PlayerCard>, IPlayerCardService
    {
        private readonly IPlayerCardRepository _cardRepository;
        private readonly IApplicationDbContext _context;

        public PlayerCardService(IPlayerCardRepository cardRepository, IApplicationDbContext context)
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

        public async Task AddAsync(PlayerCard entity)
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

        public async Task<List<PlayerCard>> GetAllAsync() => await _cardRepository.GetAll().ToListAsync();


        public async Task<PlayerCard> GetByIdAsync(Guid id) => await _cardRepository
            .GetAllEager().SingleOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _cardRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PlayerCard entity)
        {
            _cardRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PlayerCard>> GetCardsFromPlayerAsync(Guid playerId)
            => await _cardRepository.GetAllEager().Where(x => x.Player.Id == playerId && x.IsActive).ToListAsync();

        public async Task<List<PlayerCard>> GetPlayersCardsFromMatchAsync(Guid matchId)
            => await _cardRepository.GetAllEager().Where(x => x.Match.Id == matchId && x.IsActive).ToListAsync();
    }
}
