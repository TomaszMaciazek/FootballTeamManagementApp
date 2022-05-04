using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Model.Dtos.ListItemDtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using App.Model.Dtos;

namespace App.ServiceLayer.Services
{
    public interface ISurveyTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(SurveyTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<SurveyTemplate>> GetAllAsync();
        Task<SurveyWithResultsDto> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(SurveyTemplate entity);
        Task<PaginatedList<MySurveyListItemDto>> GetSurveysFromAuthor(SurveyTemplateQuery query);
        Task<PaginatedList<SurveyListItemDto>> GetSurveys(SurveyTemplateQuery query);
        Task<FillSurveyDto> GetSurveyTemplateToFill(Guid id);
        Task<IEnumerable<SurveyQuestionDto>> GetQuestionsFromSurvey(Guid surveyId);
    }

    public class SurveyTemplateService : ISurveyTemplateService
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SurveyTemplateService(ISurveyTemplateRepository surveyTemplateRepository, ISurveyQuestionRepository surveyQuestionRepository, IApplicationDbContext context, IMapper mapper)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _context = context;
            _mapper = mapper;
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

        public async Task<SurveyWithResultsDto> GetByIdAsync(Guid id) => await _surveyTemplateRepository
            .GetAll()
            .AsNoTracking()
            .Include(x => x.Questions).ThenInclude(x => x.Options)
            .Include(x => x.Questions).ThenInclude(x => x.TextQuestionAnswers).ThenInclude(x => x.UserResult)
            .Include(x => x.Questions).ThenInclude(x => x.SelectQuestionAnswers).ThenInclude(x => x.UserResult)
            .Where(x => x.Id == id)
            .ProjectTo<SurveyWithResultsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

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

        public async Task<PaginatedList<SurveyListItemDto>> GetSurveys(SurveyTemplateQuery query)
        {
            var surveys = _surveyTemplateRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.RespondentsResults);

            var result = !string.IsNullOrEmpty(query.Title) 
                ? surveys.Where(x => x.Title.ToLower().Contains(query.Title.ToLower()))
                : surveys;

            result = query.AuthorId.HasValue ? result.Where(x => x.Author.Id == query.AuthorId.Value) : result;

            result = query.IsAnonymous.HasValue ? result.Where(x => x.IsAnonymous == query.IsAnonymous.Value) : result;

            result = result.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await result.ProjectTo<SurveyListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<PaginatedList<MySurveyListItemDto>> GetSurveysFromAuthor(SurveyTemplateQuery query)
        {
            var surveys = _surveyTemplateRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.RespondentsResults)
                .Where(x => x.Author.Id == query.AuthorId.Value);

            var result = !string.IsNullOrEmpty(query.Title)
                ? surveys.Where(x => x.Title.ToLower().Contains(query.Title.ToLower()))
                : surveys;

            result = query.IsAnonymous.HasValue ? result.Where(x => x.IsAnonymous == query.IsAnonymous.Value) : result;

            result = result.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await result.ProjectTo<MySurveyListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<FillSurveyDto> GetSurveyTemplateToFill(Guid id)
        {
            return await _surveyTemplateRepository.GetAll().AsNoTracking()
            .Include(x => x.Questions).ThenInclude(x => x.Options)
            .Where(x => x.Id == id)
            .ProjectTo<FillSurveyDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SurveyQuestionDto>> GetQuestionsFromSurvey(Guid surveyId) => await _surveyQuestionRepository
            .GetAll().AsNoTracking()
            .Include(x => x.Survey)
            .Include(x => x.Options)
            .Where(x => x.Survey.Id == surveyId)
            .ProjectTo<SurveyQuestionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    }
}
