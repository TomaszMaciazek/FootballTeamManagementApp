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
    public interface IGroupChatService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(GroupChat entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<GroupChat>> GetAllAsync();
        Task<GroupChat> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(GroupChat entity);
        Task<IEnumerable<GroupChat>> GetGroupChatsFromUser(Guid userId);
    }

    public class GroupChatService : IService<GroupChat>, IGroupChatService
    {
        private readonly IGroupChatRepository _groupChatRepository;
        private readonly IApplicationDbContext _context;

        public GroupChatService(IGroupChatRepository groupChatRepository, IApplicationDbContext context)
        {
            _groupChatRepository = groupChatRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _groupChatRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(GroupChat entity)
        {
            _groupChatRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _groupChatRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GroupChat>> GetAllAsync() => await _groupChatRepository.GetAll().ToListAsync();

        public async Task<GroupChat> GetByIdAsync(Guid id) => await _groupChatRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _groupChatRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GroupChat entity)
        {
            _groupChatRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupChat>> GetGroupChatsFromUser(Guid userId) => await _groupChatRepository
            .GetAll().Include(x => x.Users)
            .Where(x => x.Users.Any(x => x.Id == userId) && x.IsActive)
            .ToListAsync();
    }
}
