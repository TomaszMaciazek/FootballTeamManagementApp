using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserRatingSurveyQuestionAnswerRepository : IRepository<UserRatingSurveyQuestionAnswer> {
        IQueryable<UserRatingSurveyQuestionAnswer> GetAllEager();
    }

    public class UserRatingSurveyQuestionAnswerRepository : BaseRepository<UserRatingSurveyQuestionAnswer>, IUserRatingSurveyQuestionAnswerRepository
    {
        public UserRatingSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserRatingSurveyQuestionAnswer> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Include(x => x.User);
    }
}
