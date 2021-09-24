using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IGroupChatImageService
    {
        Task AddAsync(GroupChatImage image);
        Task GetByChatId(Guid chatId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(GroupChatImage image);
    }

    public class GroupChatImageService : IGroupChatImageService
    {
        private readonly IGroupChatImageRepository _imageRepository;
        private readonly IApplicationDbContext _context;

        public GroupChatImageService(IGroupChatImageRepository imageRepository, IApplicationDbContext context)
        {
            _imageRepository = imageRepository;
            _context = context;
        }

        public async Task AddAsync(GroupChatImage image)
        {
            _imageRepository.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task GetByChatId(Guid chatId) => await _imageRepository
            .GetAll().FirstOrDefaultAsync(x => x.ChatId == chatId);

        public async Task UpdateAsync(GroupChatImage image)
        {
            _imageRepository.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            _imageRepository.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}
