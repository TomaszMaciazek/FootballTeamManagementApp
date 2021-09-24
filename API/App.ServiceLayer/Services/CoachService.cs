using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ICoachService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Coach entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Coach>> GetAllAsync();
        Task<Coach> GetByIdAsync(Guid id);
        Task<Coach> GetByIdEager(Guid id);
        Task<Coach> GetByUserId(Guid userId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Coach entity);
    }

    public class CoachService : IService<Coach>, ICoachService
    {

        private readonly ICoachRepository _coachRepository;
        private readonly IApplicationDbContext _context;

        public CoachService(ICoachRepository coachRepository, IApplicationDbContext context)
        {
            _coachRepository = coachRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _coachRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Coach entity)
        {
            _coachRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _coachRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Coach>> GetAllAsync() => await _coachRepository.GetAll().ToListAsync();

        public async Task<Coach> GetByIdAsync(Guid id) => await _coachRepository.GetById(id).FirstOrDefaultAsync();

        public async Task<Coach> GetByIdEager(Guid id) => await _coachRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _coachRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Coach entity)
        {
            _coachRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Coach> GetByUserId(Guid userId) => await _coachRepository.GetByUserIdEager(userId).FirstOrDefaultAsync();
    }
}
