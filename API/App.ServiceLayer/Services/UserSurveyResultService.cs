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
    public interface IUserSurveyResultService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(UserSurveyResult entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<UserSurveyResult>> GetAllAsync();
        Task<UserSurveyResult> GetByIdAsync(Guid id);
        Task<UserSurveyResult> GetUserSurveyResult(Guid userId, Guid surveyId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UserSurveyResult entity);
    }

    public class UserSurveyResultService : IService<UserSurveyResult>, IUserSurveyResultService
    {
        private readonly IUserSurveyResultRepository _userSurveyResultRepository;
        private readonly IApplicationDbContext _context;

        public UserSurveyResultService(IUserSurveyResultRepository userSurveyResultRepository, IApplicationDbContext context)
        {
            _userSurveyResultRepository = userSurveyResultRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _userSurveyResultRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(UserSurveyResult entity)
        {
            _userSurveyResultRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _userSurveyResultRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserSurveyResult>> GetAllAsync() => await _userSurveyResultRepository.GetAll().ToListAsync();

        public async Task<UserSurveyResult> GetByIdAsync(Guid id) => await _userSurveyResultRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _userSurveyResultRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSurveyResult entity)
        {
            _userSurveyResultRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserSurveyResult> GetUserSurveyResult(Guid userId, Guid surveyId) => await _userSurveyResultRepository.GetAllEager()
                .Where(x => x.User.Id == userId && x.Survey.Id == surveyId)
                .FirstOrDefaultAsync();
    }
}
