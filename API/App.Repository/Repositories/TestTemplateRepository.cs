using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface ITestTemplateRepository : IRepository<TestTemplate>
    {
    }

    public class TestTemplateRepository : BaseRepository<TestTemplate>, ITestTemplateRepository
    {
        public TestTemplateRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
