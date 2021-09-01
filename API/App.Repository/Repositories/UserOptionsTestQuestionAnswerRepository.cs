using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserOptionsTestQuestionAnswerRepository : IRepository<UserOptionsTestQuestionAnswer>
    {
        IQueryable<UserOptionsTestQuestionAnswer> GetAllEager();
    }

    public class UserOptionsTestQuestionAnswerRepository : BaseRepository<UserOptionsTestQuestionAnswer>, IUserOptionsTestQuestionAnswerRepository
    {
        public UserOptionsTestQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserOptionsTestQuestionAnswer> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Include(x => x.User);
    }
}
