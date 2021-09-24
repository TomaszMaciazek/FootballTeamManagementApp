using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserBoolSurveyQuestionAnswerRepository : IRepository<UserBoolSurveyQuestionAnswer> {
        IQueryable<UserBoolSurveyQuestionAnswer> GetAllEager();
    }
    public class UserBoolSurveyQuestionAnswerRepository : BaseRepository<UserBoolSurveyQuestionAnswer>, IUserBoolSurveyQuestionAnswerRepository
    {
        public UserBoolSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserBoolSurveyQuestionAnswer> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Include(x => x.User);
    }
}
