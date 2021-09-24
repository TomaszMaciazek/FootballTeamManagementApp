using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
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
    public interface IRatingSurveyQuestionTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(RatingSurveyQuestionTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<RatingSurveyQuestionTemplate>> GetAllAsync();
        Task<RatingSurveyQuestionTemplate> GetByIdAsync(Guid id);
        Task<List<RatingSurveyQuestionTemplate>> GetQuestions(SurveyQuestionQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(RatingSurveyQuestionTemplate entity);
    }

    public class RatingSurveyQuestionTemplateService : IService<RatingSurveyQuestionTemplate>, IRatingSurveyQuestionTemplateService
    {
        private readonly IRatingSurveyQuestionTemplateRepository _questionTemplateRepository;
        private readonly IApplicationDbContext _context;

        public RatingSurveyQuestionTemplateService(IRatingSurveyQuestionTemplateRepository questionTemplateRepository, IApplicationDbContext context)
        {
            _questionTemplateRepository = questionTemplateRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _questionTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(RatingSurveyQuestionTemplate entity)
        {
            _questionTemplateRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _questionTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RatingSurveyQuestionTemplate>> GetAllAsync() => await _questionTemplateRepository.GetAll().ToListAsync();

        public async Task<RatingSurveyQuestionTemplate> GetByIdAsync(Guid id) => await _questionTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _questionTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RatingSurveyQuestionTemplate entity)
        {
            _questionTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RatingSurveyQuestionTemplate>> GetQuestions(SurveyQuestionQuery query)
        {
            var questions = _questionTemplateRepository.GetAllEager()
                .Where(x => x.IsActive);

            questions = questions.WhereGuidPropertyEquals(x => x.Survey.Id, query.SurveyId);

            questions = questions.WhereIntPropertyEquals(x => x.PageNumber, query.SurveyPage);

            return await questions.ToListAsync();
        }
    }
}
