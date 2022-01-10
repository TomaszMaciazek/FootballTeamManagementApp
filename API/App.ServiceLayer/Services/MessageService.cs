using App.DataAccess.Exceptions;
using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.Enums;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IMessageService
    {
        Task<bool> ActivateAsync(Guid id);
        Task ChangeMessagesTransmissionsStatus(ChangeMessagesStatusVM command);
        Task CreateMessageAsync(CreateMessageVM command);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Message>> GetAllAsync();
        Task<SimpleMessageDto> GetByIdAsync(Guid id);
        Task<PaginatedList<MessageDto>> GetMessagesFromUserAsync(MessageQuery query);
        Task<MessageDto> GetMessageTransmissionByIdAsync(Guid id);
        Task<int> GetNumberOfUnreadMessages(Guid userId);
        Task RemoveUserMessagesAsync(Guid userId, IEnumerable<Guid> ids);
        Task TakeMessagesFromTrash(Guid userId, IEnumerable<Guid> ids);
        Task UpdateMessageAsync(UpdateMessageVM entity);
        Task UpdateMessageTransmissionAsync(UpdateMessageTransmissionVM command);
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageTransmissionRepository _messageTransmissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMessageTransmissionRepository messageTransmissionRepository, IUserRepository userRepository, IApplicationDbContext context, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _messageTransmissionRepository = messageTransmissionRepository;
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _messageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateMessageAsync(CreateMessageVM command)
        {
            var sender = await _userRepository.GetById(command.SenderId).SingleOrDefaultAsync();

            if (sender == null)
            {
                throw new NotFoundException();
            }

            var transmissions = new List<MessageTransmission>();

            var entity = new Message
            {
                Sender = sender,
                SendDate = DateTime.Now,
                Topic = command.Topic,
                Content = command.Content,
                IsActive = true,
            };

            var recipientsUsers = await _userRepository.GetAll().Where(x => command.RecipientsIds.Contains(x.Id)).ToListAsync();

            var recipients = new List<MessageRecipient>();

            foreach (var recipient in recipientsUsers)
            {

                recipients.Add(new MessageRecipient
                {
                    Recipient = recipient
                });

                transmissions.Add(new MessageTransmission()
                {
                    MailboxOwner = recipient,
                    IsActive = true,
                    MailboxType = MailboxType.Inbox
                });
            }


            transmissions.Add(new MessageTransmission
            {
                MailboxOwner = sender,
                IsActive = true,
                MailboxType = MailboxType.Sent
            });

            entity.Recipients = recipients;
            entity.Transmissions = transmissions;


            _messageRepository.Add(entity);

            await _context.SaveChangesAsync();
        }


        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _messageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<Message>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SimpleMessageDto> GetByIdAsync(Guid id) => await _messageRepository.GetAll()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .ProjectTo<SimpleMessageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task<MessageDto> GetMessageTransmissionByIdAsync(Guid id) => await _messageTransmissionRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.Message).ThenInclude(x => x.Recipients)
            .Include(x => x.Message).ThenInclude(x => x.Sender)
            .Where(x => x.Id == id)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task<PaginatedList<MessageDto>> GetMessagesFromUserAsync(MessageQuery query)
        {
            var messages = _context.MessageTransmissions
                .Include(m => m.MailboxOwner)
                .Include(m => m.Message)
                .ThenInclude(m => m.Sender)
                .Include(m => m.Message)
                .ThenInclude(m => m.Recipients)
                .ThenInclude(m => m.Recipient)
                .AsNoTracking()
                .Where(x => x.MailboxOwner.Id == query.UserId && x.MailboxType == query.MailboxType && x.IsActive);

            if (!string.IsNullOrEmpty(query.SearchText))
            {
                messages = messages.Where(
                            x => x.Message.Topic.ToLower().Contains(query.SearchText.ToLower()) ||
                            x.Message.SendDate.ToString().Contains(query.SearchText));
            }
            if (query.OrderByColumnName.ToLower() == "date")
            {
                messages = query.OrderByDirection == "asc"
                ? messages.OrderBy(x => x.Message.SendDate)
                : messages.OrderByDescending(x => x.Message.SendDate);
            }
            else if (query.OrderByColumnName.ToLower() == "topic")
            {
                messages = query.OrderByDirection == "asc"
                ? messages.OrderBy(x => x.Message.Topic)
                : messages.OrderByDescending(x => x.Message.Topic);
            }
            else
            {
                messages = messages.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }

            return await messages.ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task RemoveUserMessagesAsync(Guid userId, IEnumerable<Guid> ids)
        {
            var transmissions = await _messageTransmissionRepository.GetAll()
               .Include(x => x.MailboxOwner)
               .Include(x => x.Message)
               .Where(x => x.MailboxOwner.Id == userId && ids.Contains(x.Message.Id))
               .ToListAsync();

            foreach (var transmission in transmissions)
            {
                switch (transmission.MailboxType)
                {
                    case MailboxType.Inbox:
                        transmission.MailboxType = MailboxType.Trash;
                        break;
                    case MailboxType.Sent:
                        transmission.MailboxType = MailboxType.Trash;
                        break;
                    case MailboxType.Trash:
                        _messageTransmissionRepository.Remove(transmission.Id);
                        break;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessageAsync(UpdateMessageVM entity)
        {
            var message = await _messageRepository.GetById(entity.Id).SingleOrDefaultAsync();
            if (message == null)
            {
                throw new NotFoundException();
            }

            message.IsActive = entity.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessageTransmissionAsync(UpdateMessageTransmissionVM command)
        {
            var transmission = await _messageTransmissionRepository.GetById(command.Id).SingleOrDefaultAsync();
            if (transmission == null)
            {
                throw new NotFoundException();
            }

            if (command.IsActive.HasValue)
            {
                transmission.IsActive = command.IsActive.Value;
            }

            if (command.MailboxType.HasValue)
            {
                transmission.MailboxType = command.MailboxType.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetNumberOfUnreadMessages(Guid userId)
        {

            return await _messageTransmissionRepository.GetAll()
                .Include(x => x.MailboxOwner)
                .Where(x => x.MailboxType == MailboxType.Inbox && x.MailboxOwner.Id == userId && x.IsRead == false)
                .CountAsync();
        }

        public async Task ChangeMessagesTransmissionsStatus(ChangeMessagesStatusVM command)
        {
            var transmissions = await _messageTransmissionRepository.GetAll()
                .Include(x => x.MailboxOwner)
                .Include(x => x.Message)
                .Where(x => x.MailboxOwner.Id == command.UserId && command.MessagesIds.Contains(x.Message.Id))
                .ToListAsync();

            foreach (var transmission in transmissions)
            {
                transmission.IsRead = command.IsRead;
            }

            await _context.SaveChangesAsync();
        }

        public async Task TakeMessagesFromTrash(Guid userId, IEnumerable<Guid> ids)
        {
            var transmissions = await _messageTransmissionRepository.GetAll()
              .Include(x => x.MailboxOwner)
              .Include(x => x.Message).ThenInclude(x => x.Sender)
              .Where(x => x.MailboxOwner.Id == userId && ids.Contains(x.Message.Id) && x.MailboxType == MailboxType.Trash)
              .ToListAsync();

            foreach (var transmission in transmissions)
            {
                if (transmission.Message.Sender.Id == userId)
                {
                    transmission.MailboxType = MailboxType.Sent;
                }
                else
                {
                    transmission.MailboxType = MailboxType.Inbox;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
