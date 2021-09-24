using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.ServiceLayer.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IGroupMessageService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(GroupMessage entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<GroupMessage>> GetAllAsync();
        Task<GroupMessage> GetByIdAsync(Guid id);
        Task<PaginatedList<GroupMessage>> GetMessagesAsync(MessageQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(GroupMessage entity);
    }

    public class GroupMessageService : IService<GroupMessage>, IGroupMessageService
    {

        private readonly IGroupMessageRepository _groupMessageRepository;
        private readonly IApplicationDbContext _context;

        public GroupMessageService(IGroupMessageRepository groupMessageRepository, IApplicationDbContext context)
        {
            _groupMessageRepository = groupMessageRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _groupMessageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(GroupMessage entity)
        {
            _groupMessageRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _groupMessageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GroupMessage>> GetAllAsync() => await _groupMessageRepository.GetAll().ToListAsync();

        public async Task<GroupMessage> GetByIdAsync(Guid id) => await _groupMessageRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _groupMessageRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GroupMessage entity)
        {
            _groupMessageRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<GroupMessage>> GetMessagesAsync(MessageQuery query)
        {
            var messages = _groupMessageRepository.GetAllEager().Where(x => x.IsActive);

            messages = messages.WhereStringPropertyContains(x => x.Content, query.Phrase);

            messages = messages.WhereGuidPropertyEquals(x => x.Sender.Id, query.SenderId);

            messages = messages.WhereGuidPropertyEquals(x => x.Chat.Id, query.ChatId);

            messages = messages.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await messages.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
