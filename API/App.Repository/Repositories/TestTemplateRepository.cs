using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITestTemplateRepository : IRepository<TestTemplate>
    {
        IQueryable<TestTemplate> GetAllEager();

        TestTemplate GetByIdEager(Guid id);
    }

    public class TestTemplateRepository : BaseRepository<TestTemplate>, ITestTemplateRepository
    {
        public TestTemplateRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TestTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.BoolTestQuestions)
            .Include(x => x.OptionsTestQuestions).ThenInclude(x => x.Answers);

        public TestTemplate GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.BoolTestQuestions)
            .Include(x => x.OptionsTestQuestions).ThenInclude(x => x.Answers)
            .FirstOrDefault(x => x.Id == id);
    }
}
