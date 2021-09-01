using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserBoolTestQuestionAnswerRepository : IRepository<UserBoolTestQuestionAnswer>
    {
        IQueryable<UserBoolTestQuestionAnswer> GetAllEager();
    }

    public class UserBoolTestQuestionAnswerRepository : BaseRepository<UserBoolTestQuestionAnswer>, IUserBoolTestQuestionAnswerRepository
    {

        public UserBoolTestQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserBoolTestQuestionAnswer> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Include(x => x.User);
    }
}
