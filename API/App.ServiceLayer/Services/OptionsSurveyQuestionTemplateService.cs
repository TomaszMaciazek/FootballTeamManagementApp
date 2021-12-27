using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.Model.ViewModels.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IOptionsSurveyQuestionTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(OptionsSurveyQuestionTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<OptionsSurveyQuestionTemplate>> GetAllAsync();
        Task<OptionsSurveyQuestionTemplate> GetByIdAsync(Guid id);
        Task<List<OptionsSurveyQuestionTemplate>> GetQuestions(SurveyQuestionQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(OptionsSurveyQuestionTemplate entity);
    }

    public class OptionsSurveyQuestionTemplateService : IService<OptionsSurveyQuestionTemplate>, IOptionsSurveyQuestionTemplateService
    {
        private readonly IOptionsSurveyQuestionTemplateRepository _questionTemplateRepository;
        private readonly IApplicationDbContext _context;

        public OptionsSurveyQuestionTemplateService(IOptionsSurveyQuestionTemplateRepository questionTemplateRepository, IApplicationDbContext context)
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

        public async Task AddAsync(OptionsSurveyQuestionTemplate entity)
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

        public async Task<List<OptionsSurveyQuestionTemplate>> GetAllAsync() => await _questionTemplateRepository.GetAll().ToListAsync();

        public async Task<OptionsSurveyQuestionTemplate> GetByIdAsync(Guid id) => await _questionTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _questionTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OptionsSurveyQuestionTemplate entity)
        {
            _questionTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OptionsSurveyQuestionTemplate>> GetQuestions(SurveyQuestionQuery query)
        {
            var questions = _questionTemplateRepository.GetAllEager()
                .Where(x => x.IsActive);

            questions = questions.WhereGuidPropertyEquals(x => x.Survey.Id, query.SurveyId);

            questions = questions.WhereIntPropertyEquals(x => x.PageNumber, query.SurveyPage);

            return await questions.ToListAsync();
        }
    }
}
