using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities.TestEntities;
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
using System.Text;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserTestResultService
    {
        Task AddAsync(UserTestResult entity);
        Task<IEnumerable<SimpleUserDto>> GetTestRespondents(Guid TestId);
        Task<UserTestResultDto> GetByIdAsync(Guid id);
        Task<PaginatedList<UserTestResultListItemDto>> GetUserResults(UserTestResultQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(FillTestVM command);
    }

    public class UserTestResultService : IUserTestResultService
    {
        private readonly IUserTestResultRepository _userTestResultRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserTestResultService(IUserTestResultRepository userTestResultRepository, ITestQuestionRepository testQuestionRepository, IApplicationDbContext context, IMapper mapper)
        {
            _userTestResultRepository = userTestResultRepository;
            _testQuestionRepository = testQuestionRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(UserTestResult entity)
        {
            _userTestResultRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserTestResultDto> GetByIdAsync(Guid id)
        {
            return await _userTestResultRepository
            .GetAll()
            .AsNoTracking()
            .Include(x => x.Test)
            .Include(x => x.QuestionAnswers)
            .Where(x => x.Id == id)
            .ProjectTo<UserTestResultDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            _userTestResultRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FillTestVM command)
        {
            var result = await _userTestResultRepository.GetAll()
                .Include(x => x.QuestionAnswers)
                .Include(x => x.Test)
                .Where(x => x.Test.Id == command.TestId && x.User.Id == command.RespondentId)
                .FirstOrDefaultAsync();

            var questionsIds = command.QuestionAnswers.Select(x => x.QuestionId);

            var questions = await _testQuestionRepository.GetAll().Include(x => x.Options).Where(x => questionsIds.Contains(x.Id)).ToListAsync();

            var questionsAnswers = new List<TestQuestionAnswer>();

            double pointsSum = 0;

            var answersGroupedByQuestion = command.QuestionAnswers.GroupBy(x => x.QuestionId);

            foreach(var group in answersGroupedByQuestion)
            {
                double sum = 0;
                var question = questions.FirstOrDefault(x => x.Id == group.Key);
                if (question != null)
                {
                    foreach(var answer in group.Select(x => x))
                    {
                        var option = question.Options.FirstOrDefault(x => x.Value == x.Value);
                        if(option != null)
                        {
                            sum += option.Points;
                            questionsAnswers.Add(new TestQuestionAnswer { Question = question, AnswerValue = answer.Value });
                        }
                    }
                }

                pointsSum = sum > 0 ? pointsSum + sum : pointsSum;
            }

            result.ScoredPoints = pointsSum;
            result.Passed = pointsSum >= result.Test.PassedMinimalValue;

            result.QuestionAnswers = questionsAnswers;
            result.IsCompleated = true;
            _userTestResultRepository.Update(result);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<UserTestResultListItemDto>> GetUserResults(UserTestResultQuery query)
        {
            var results = _userTestResultRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Test).ThenInclude(x => x.Author)
                .Where(x => x.User.Id == query.UserId);

            results = query.IsCompleated.HasValue ? results.Where(x => x.IsCompleated == query.IsCompleated.Value) : results;

            results = !string.IsNullOrEmpty(query.Title)
                ? results.Where(x => x.Test.Title.ToLower().Contains(query.Title.ToLower()))
                : results;

            if (query.OrderByColumnName.ToLower() == "title")
            {

                results = query.OrderByDirection == "asc"
                    ? results.OrderBy(x => x.Test.Title)
                    : results.OrderByDescending(x => x.Test.Title);
            }
            else
            {
                results = results.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }


            return await results.ProjectTo<UserTestResultListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<IEnumerable<SimpleUserDto>> GetTestRespondents(Guid TestId) => await _userTestResultRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.Test.Id == TestId)
            .Select(x => x.User)
            .ProjectTo<SimpleUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
