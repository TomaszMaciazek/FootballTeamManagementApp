using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserRatingSurveyQuestionAnswerService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(UserRatingSurveyQuestionAnswer entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<UserRatingSurveyQuestionAnswer>> GetAllAsync();
        Task<List<UserRatingSurveyQuestionAnswer>> GetAnswers(SurveyAnswerQuery query);
        Task<UserRatingSurveyQuestionAnswer> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UserRatingSurveyQuestionAnswer entity);
    }

    public class UserRatingSurveyQuestionAnswerService : IService<UserRatingSurveyQuestionAnswer>, IUserRatingSurveyQuestionAnswerService
    {
        private readonly IUserRatingSurveyQuestionAnswerRepository _answersTemplateRepository;
        private readonly IApplicationDbContext _context;

        public UserRatingSurveyQuestionAnswerService(IUserRatingSurveyQuestionAnswerRepository answersTemplateRepository, IApplicationDbContext context)
        {
            _answersTemplateRepository = answersTemplateRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _answersTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(UserRatingSurveyQuestionAnswer entity)
        {
            _answersTemplateRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _answersTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserRatingSurveyQuestionAnswer>> GetAllAsync() => await _answersTemplateRepository.GetAll().ToListAsync();

        public async Task<UserRatingSurveyQuestionAnswer> GetByIdAsync(Guid id) => await _answersTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _answersTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRatingSurveyQuestionAnswer entity)
        {
            _answersTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserRatingSurveyQuestionAnswer>> GetAnswers(SurveyAnswerQuery query)
        {
            var answers = _answersTemplateRepository.GetAllEager().Where(x => x.IsActive);

            answers = answers.WhereGuidPropertyEquals(x => x.User.Id, query.UserId);

            answers = answers.WhereCollectionContainsPropertyValue(x => x.Question.Id, query.QuestionsIds);

            return await answers.ToListAsync();
        }
    }
}
