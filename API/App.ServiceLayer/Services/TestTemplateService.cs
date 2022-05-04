using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities.TestEntities;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
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
    public interface ITestTemplateService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(TestTemplate entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<TestTemplate>> GetAllAsync();
        Task<TestTemplateDto> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(TestTemplate entity);
        Task<PaginatedList<TestListItemDto>> GetTests(TestTemplateQuery query);
        Task<PaginatedList<MyTestListItemDto>> GetTestsFromAuthor(TestTemplateQuery query);
        Task<FillTestDto> GetTestTemplateToFill(Guid id);
        Task<IEnumerable<TestQuestionDto>> GetQuestionsFromTest(Guid testId);

    }

    public class TestTemplateService : ITestTemplateService
    {
        private readonly ITestTemplateRepository _testTemplateRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TestTemplateService(ITestTemplateRepository testTemplateRepository, ITestQuestionRepository testQuestionRepository, IApplicationDbContext context, IMapper mapper)
        {
            _testTemplateRepository = testTemplateRepository;
            _testQuestionRepository = testQuestionRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _testTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(TestTemplate entity)
        {
            _testTemplateRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _testTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TestTemplate>> GetAllAsync() => await _testTemplateRepository.GetAll().ToListAsync();

        public async Task<TestTemplateDto> GetByIdAsync(Guid id) => await  _testTemplateRepository.GetAll().AsNoTracking()
            .Include(x => x.Questions).ThenInclude(x => x.Options)
            .Where(x => x.Id == id)
            .ProjectTo<TestTemplateDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _testTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TestTemplate entity)
        {
            _testTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<TestListItemDto>> GetTests(TestTemplateQuery query)
        {
            var Tests = _testTemplateRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.UserResults);

            var result = !string.IsNullOrEmpty(query.Title)
                ? Tests.Where(x => x.Title.ToLower().Contains(query.Title.ToLower()))
                : Tests;

            result = query.AuthorId.HasValue ? result.Where(x => x.Author.Id == query.AuthorId.Value) : result;

            result = result.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await result.ProjectTo<TestListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<PaginatedList<MyTestListItemDto>> GetTestsFromAuthor(TestTemplateQuery query)
        {
            var tests = _testTemplateRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.UserResults)
                .Where(x => x.Author.Id == query.AuthorId.Value);

            var result = !string.IsNullOrEmpty(query.Title)
                ? tests.Where(x => x.Title.ToLower().Contains(query.Title.ToLower()))
                : tests;

            result = result.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await result.ProjectTo<MyTestListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<FillTestDto> GetTestTemplateToFill(Guid id)
        {
            return await _testTemplateRepository.GetAll().AsNoTracking()
            .Include(x => x.Questions).ThenInclude(x => x.Options)
            .Where(x => x.Id == id)
            .ProjectTo<FillTestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TestQuestionDto>> GetQuestionsFromTest(Guid testId) => await _testQuestionRepository
            .GetAll().AsNoTracking()
            .Include(x => x.Test)
            .Include(x => x.Options)
            .Where(x => x.Test.Id == testId)
            .ProjectTo<TestQuestionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
