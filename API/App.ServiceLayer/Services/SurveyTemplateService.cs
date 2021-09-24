using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.ServiceLayer.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ISurveyTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(SurveyTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<SurveyTemplate>> GetAllAsync();
        Task<SurveyTemplate> GetByIdAsync(Guid id);
        Task<PaginatedList<SurveyTemplate>> GetSurveysFromAuthor(Guid authorId, SurveyTemplateQuery query);
        Task<PaginatedList<SurveyTemplate>> GetSurveysFromRespondent(Guid authorId, SurveyTemplateQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(SurveyTemplate entity);
    }

    public class SurveyTemplateService : IService<SurveyTemplate>, ISurveyTemplateService
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        private readonly IApplicationDbContext _context;

        public SurveyTemplateService(ISurveyTemplateRepository surveyTemplateRepository, IApplicationDbContext context)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _surveyTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(SurveyTemplate entity)
        {
            _surveyTemplateRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _surveyTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SurveyTemplate>> GetAllAsync() => await _surveyTemplateRepository.GetAll().ToListAsync();

        public async Task<SurveyTemplate> GetByIdAsync(Guid id) => await _surveyTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _surveyTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SurveyTemplate entity)
        {
            _surveyTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<SurveyTemplate>> GetSurveysFromAuthor(Guid authorId, SurveyTemplateQuery query)
        {
            var surveys = _surveyTemplateRepository.GetAllEager().Where(x => x.IsActive && x.Author.Id == authorId);

            surveys = surveys.WhereStringPropertyContains(x => x.Title, query.Title);

            surveys = surveys.WhereBoolPropertyEquals(x => x.IsAnonymous, query.IsAnonymous);

            surveys = surveys.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await surveys.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<PaginatedList<SurveyTemplate>> GetSurveysFromRespondent(Guid respondentId, SurveyTemplateQuery query)
        {
            var surveys = _surveyTemplateRepository.GetAllEager().Where(x => x.IsActive && x.RespondentsResults.Any(x => x.User.Id == respondentId));

            surveys = surveys.WhereStringPropertyContains(x => x.Title, query.Title);

            surveys = surveys.WhereBoolPropertyEquals(x => x.IsAnonymous, query.IsAnonymous);

            surveys = surveys.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await surveys.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

    }
}
