using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserImageService
    {
        Task AddAsync(UserImage image);
        Task GetByUserId(Guid userId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UserImage image);
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

        public async Task AddAsync(UserImage image)
        {
            _userImageRepository.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task GetByUserId(Guid userId) => await _userImageRepository
            .GetAll().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task UpdateAsync(UserImage image)
        {
            _userImageRepository.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            _userImageRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

    }
}
