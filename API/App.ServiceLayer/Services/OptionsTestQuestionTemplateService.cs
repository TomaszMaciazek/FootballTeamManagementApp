using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.QuestionTemplates;
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
    public interface IOptionsTestQuestionTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(OptionsTestQuestionTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<OptionsTestQuestionTemplate>> GetAllAsync();
        Task<OptionsTestQuestionTemplate> GetByIdAsync(Guid id);
        Task<List<OptionsTestQuestionTemplate>> GetQuestions(TestQuestionQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(OptionsTestQuestionTemplate entity);
    }

    public class OptionsTestQuestionTemplateService : IService<OptionsTestQuestionTemplate>, IOptionsTestQuestionTemplateService
    {
        private readonly IOptionsTestQuestionTemplateRepository _questionTemplateRepository;
        private readonly IApplicationDbContext _context;

        public OptionsTestQuestionTemplateService(IOptionsTestQuestionTemplateRepository questionTemplateRepository, IApplicationDbContext context)
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

        public async Task AddAsync(OptionsTestQuestionTemplate entity)
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

        public async Task<List<OptionsTestQuestionTemplate>> GetAllAsync() => await _questionTemplateRepository.GetAll().ToListAsync();

        public async Task<OptionsTestQuestionTemplate> GetByIdAsync(Guid id) => await _questionTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _questionTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OptionsTestQuestionTemplate entity)
        {
            _questionTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OptionsTestQuestionTemplate>> GetQuestions(TestQuestionQuery query)
        {
            var questions = _questionTemplateRepository.GetAllEager()
                .Where(x => x.IsActive);

            questions = questions.WhereGuidPropertyEquals(x => x.Test.Id, query.TestId);

            questions = questions.WhereIntPropertyEquals(x => x.PageNumber, query.Page);

            return await questions.ToListAsync();
        }
    }
}
