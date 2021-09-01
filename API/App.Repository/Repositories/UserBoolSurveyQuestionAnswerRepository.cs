using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IUserBoolSurveyQuestionAnswerRepository : IRepository<UserBoolSurveyQuestionAnswer> { }
    public class UserBoolSurveyQuestionAnswerRepository : BaseRepository<UserBoolSurveyQuestionAnswer>, IUserBoolSurveyQuestionAnswerRepository
    {
        public UserBoolSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
