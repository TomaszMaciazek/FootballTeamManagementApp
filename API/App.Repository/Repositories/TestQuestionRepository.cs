using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface ITestQuestionRepository : IRepository<TestQuestion> { }
    public class TestQuestionRepository : BaseRepository<TestQuestion>, ITestQuestionRepository
    {
        public TestQuestionRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
