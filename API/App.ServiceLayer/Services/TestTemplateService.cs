using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
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
        Task<TestTemplate> GetByIdAsync(Guid id);
        Task<PaginatedList<TestTemplate>> GetTestsFromAuthor(Guid authorId, TestTemplateQuery query);
        Task<PaginatedList<TestTemplate>> GetTestsFromRespondent(Guid respondentId, TestTemplateQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(TestTemplate entity);
    }

    public class TestTemplateService : IService<TestTemplate>, ITestTemplateService
    {
        private readonly ITestTemplateRepository _testTemplateRepository;
        private readonly IApplicationDbContext _context;

        public TestTemplateService(ITestTemplateRepository testTemplateRepository, IApplicationDbContext context)
        {
            _testTemplateRepository = testTemplateRepository;
            _context = context;
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

        public async Task<TestTemplate> GetByIdAsync(Guid id) => await _testTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<PaginatedList<TestTemplate>> GetTestsFromAuthor(Guid authorId, TestTemplateQuery query)
        {
            var surveys = _testTemplateRepository.GetAllEager().Where(x => x.IsActive && x.Author.Id == authorId);

            surveys = surveys.WhereStringPropertyContains(x => x.Title, query.Title);

            surveys = surveys.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await surveys.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<PaginatedList<TestTemplate>> GetTestsFromRespondent(Guid respondentId, TestTemplateQuery query)
        {
            var surveys = _testTemplateRepository.GetAllEager().Where(x => x.IsActive && x.UserResults.Any(x => x.User.Id == respondentId));

            surveys = surveys.WhereStringPropertyContains(x => x.Title, query.Title);

            surveys = surveys.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await surveys.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
