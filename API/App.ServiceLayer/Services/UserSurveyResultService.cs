using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.Entities.SurveyEntities;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserSurveyResultService
    {
        Task AddAsync(UserSurveyResult entity);
        Task<List<UserSurveyResult>> GetAllAsync();
        Task<UserSurveyResultDto> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(FillSurveyVM command);
        Task<PaginatedList<UserSurveyResultListItemDto>> GetUserResults(UserSurveyResultQuery query);
        Task<IEnumerable<SimpleUserDto>> GetSurveyRespondents(Guid surveyId);
    }

    public class UserSurveyResultService : IUserSurveyResultService
    {
        private readonly IUserSurveyResultRepository _userSurveyResultRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserSurveyResultService(IUserSurveyResultRepository userSurveyResultRepository, ISurveyQuestionRepository surveyQuestionRepository, IApplicationDbContext context, IMapper mapper)
        {
            _userSurveyResultRepository = userSurveyResultRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(UserSurveyResult entity)
        {
            _userSurveyResultRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserSurveyResult>> GetAllAsync() => await _userSurveyResultRepository.GetAll().ToListAsync();

        public async Task<UserSurveyResultDto> GetByIdAsync(Guid id)
        {
            return await _userSurveyResultRepository
            .GetAll()
            .AsNoTracking()
            .Include(x => x.Survey)
            .Include(x => x.TextQuestionAnswers)
            .Include(x => x.SelectQuestionAnswers)
            .Where(x => x.Id == id)
            .ProjectTo<UserSurveyResultDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            _userSurveyResultRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FillSurveyVM command)
        {
            var result = await _userSurveyResultRepository.GetAll()
                .Include(x => x.SelectQuestionAnswers)
                .Include(x => x.TextQuestionAnswers)
                .Where(x => x.Survey.Id == command.SurveyId && x.User.Id == command.RespondentId)
                .FirstOrDefaultAsync();

            var questionsIds = command.SelectQuestionAnswers.Select(x => x.QuestionId).Concat(command.TextQuestionAnswers.Select(x => x.QuestionId)).Distinct();

            var questions = await _surveyQuestionRepository.GetAll().Where(x => questionsIds.Contains(x.Id)).ToListAsync();

            var textQuestionsAnswers = new List<SurveyTextQuestionAnswer>();
            var selectQuestionsAnswers = new List<SurveySelectQuestionAnswer>();

            foreach (var answer in command.TextQuestionAnswers)
            {
                var question = questions.FirstOrDefault(x => x.Id == answer.QuestionId);
                textQuestionsAnswers.Add(new SurveyTextQuestionAnswer { Question = question, AnswerValue = answer.Value });
            }

            result.TextQuestionAnswers = textQuestionsAnswers;

            foreach(var answer in command.SelectQuestionAnswers)
            {
                var question = questions.FirstOrDefault(x => x.Id == answer.QuestionId);
                selectQuestionsAnswers.Add(new SurveySelectQuestionAnswer { Question = question, AnswerValue = answer.Value });
            }

            result.SelectQuestionAnswers = selectQuestionsAnswers;
            result.IsCompleated = true;
            _userSurveyResultRepository.Update(result);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<UserSurveyResultListItemDto>> GetUserResults(UserSurveyResultQuery query)
        {
            var results = _userSurveyResultRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Survey).ThenInclude(x => x.Author)
                .Where(x => x.User.Id == query.UserId);

            results = query.IsCompleated.HasValue ? results.Where(x => x.IsCompleated == query.IsCompleated.Value) : results;

            results = !string.IsNullOrEmpty(query.Title)
                ? results.Where(x => x.Survey.Title.ToLower().Contains(query.Title.ToLower()))
                : results;

            if (query.OrderByColumnName.ToLower() == "title")
            {

                results = query.OrderByDirection == "asc"
                    ? results.OrderBy(x => x.Survey.Title)
                    : results.OrderByDescending(x => x.Survey.Title);
            }
            else
            {
                results = results.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }


            return await results.ProjectTo<UserSurveyResultListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<IEnumerable<SimpleUserDto>> GetSurveyRespondents(Guid surveyId) => await _userSurveyResultRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.Survey.Id == surveyId)
            .Select(x => x.User)
            .ProjectTo<SimpleUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
