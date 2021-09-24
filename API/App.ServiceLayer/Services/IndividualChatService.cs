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
    public interface IIndividualChatService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(IndividualChat entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<IndividualChat>> GetAllAsync();
        Task<IndividualChat> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(IndividualChat entity);
    }

    public class IndividualChatService : IService<IndividualChat>, IIndividualChatService
    {
        private readonly IIndividualChatRepository _individualChatRepository;
        private readonly IApplicationDbContext _context;

        public IndividualChatService(IIndividualChatRepository individualChatRepository, IApplicationDbContext dbContext)
        {
            _individualChatRepository = individualChatRepository;
            _context = dbContext;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _individualChatRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(IndividualChat entity)
        {
            _individualChatRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _individualChatRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<IndividualChat>> GetAllAsync() => await _individualChatRepository.GetAll().ToListAsync();

        public async Task<IndividualChat> GetByIdAsync(Guid id) => await _individualChatRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _individualChatRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IndividualChat entity)
        {
            _individualChatRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndividualChat>> GetGroupChatsFromUser(Guid userId) => await _individualChatRepository
            .GetAll().Include(x => x.Users)
            .Where(x => x.Users.Any(x => x.Id == userId) && x.IsActive)
            .ToListAsync();
    }
}
