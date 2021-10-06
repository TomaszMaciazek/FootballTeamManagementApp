using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserImageService
    {
        Task<UserImage> AddAsync(UserImage image);
        Task<UserImage> GetByUserId(Guid userId);
        Task RemoveAsync(Guid id);
        Task<UserImage> UpdateAsync(Guid id, byte[] data);
    }

    public class UserImageService : IUserImageService
    {
        private readonly IUserImageRepository _userImageRepository;
        private readonly IApplicationDbContext _context;

        public UserImageService(IUserImageRepository userImageRepository, IApplicationDbContext context)
        {
            _userImageRepository = userImageRepository;
            _context = context;
        }

        public async Task<UserImage> AddAsync(UserImage image)
        {
            _userImageRepository.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<UserImage> GetByUserId(Guid userId) => await _userImageRepository
            .GetAll().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<UserImage> UpdateAsync(Guid id, byte[] data)
        {
            var image = await _userImageRepository.GetById(id).SingleOrDefaultAsync();
            image.Data = data;
            _userImageRepository.Update(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task RemoveAsync(Guid id)
        {
            _userImageRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

    }
}
