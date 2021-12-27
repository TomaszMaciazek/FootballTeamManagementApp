using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IBoolSurveyQuestionTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(BoolSurveyQuestionTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<BoolSurveyQuestionTemplate>> GetAllAsync();
        Task<BoolSurveyQuestionTemplate> GetByIdAsync(Guid id);
        Task<List<BoolSurveyQuestionTemplate>> GetQuestions(SurveyQuestionQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(BoolSurveyQuestionTemplate entity);
    }

    public class BoolSurveyQuestionTemplateService : IService<BoolSurveyQuestionTemplate>, IBoolSurveyQuestionTemplateService
    {
        private readonly IBoolSurveyQuestionTemplateRepository _questionTemplateRepository;
        private readonly IApplicationDbContext _context;

        public BoolSurveyQuestionTemplateService(IBoolSurveyQuestionTemplateRepository questionTemplateRepository, IApplicationDbContext context)
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

        public async Task AddAsync(BoolSurveyQuestionTemplate entity)
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

        public async Task<List<BoolSurveyQuestionTemplate>> GetAllAsync() => await _questionTemplateRepository.GetAll().ToListAsync();

        public async Task<BoolSurveyQuestionTemplate> GetByIdAsync(Guid id) => await _questionTemplateRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _questionTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BoolSurveyQuestionTemplate entity)
        {
            _questionTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BoolSurveyQuestionTemplate>> GetQuestions(SurveyQuestionQuery query)
        {
            var questions = _questionTemplateRepository.GetAllEager()
                .Where(x => x.IsActive);

            questions = questions.WhereGuidPropertyEquals(x => x.Survey.Id, query.SurveyId);

            questions = questions.WhereIntPropertyEquals(x => x.PageNumber, query.SurveyPage);

            return await questions.ToListAsync();
        }
    }
}
