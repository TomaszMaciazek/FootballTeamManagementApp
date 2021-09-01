using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IUserRatingSurveyQuestionAnswerRepository : IRepository<UserRatingSurveyQuestionAnswer> { }

    public class UserRatingSurveyQuestionAnswerRepository : BaseRepository<UserRatingSurveyQuestionAnswer>, IUserRatingSurveyQuestionAnswerRepository
    {
        public UserRatingSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
