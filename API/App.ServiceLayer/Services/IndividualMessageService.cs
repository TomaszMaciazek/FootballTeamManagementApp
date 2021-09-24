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
    public interface IIndividualMessageService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(IndividualMessage entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<IndividualMessage>> GetAllAsync();
        Task<IndividualMessage> GetByIdAsync(Guid id);
        Task<PaginatedList<IndividualMessage>> GetMessagesAsync(MessageQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(IndividualMessage entity);
    }

    public class IndividualMessageService : IService<IndividualMessage>, IIndividualMessageService
    {
        private readonly IIndividualMessageRepository _individualMessageRepository;
        private readonly IApplicationDbContext _context;

        public IndividualMessageService(IIndividualMessageRepository individualMessageRepository, IApplicationDbContext context)
        {
            _individualMessageRepository = individualMessageRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _individualMessageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(IndividualMessage entity)
        {
            _individualMessageRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _individualMessageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<IndividualMessage>> GetAllAsync() => await _individualMessageRepository.GetAll().ToListAsync();

        public async Task<IndividualMessage> GetByIdAsync(Guid id) => await _individualMessageRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _individualMessageRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IndividualMessage entity)
        {
            _individualMessageRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<IndividualMessage>> GetMessagesAsync(MessageQuery query)
        {
            var messages = _individualMessageRepository.GetAllEager().Where(x => x.IsActive);

            messages = messages.WhereStringPropertyContains(x => x.Content, query.Phrase);

            messages = messages.WhereGuidPropertyEquals(x => x.Sender.Id, query.SenderId);

            messages = messages.WhereGuidPropertyEquals(x => x.Chat.Id, query.ChatId);

            messages = messages.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await messages.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
