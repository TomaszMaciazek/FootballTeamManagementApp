using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{ 

    public interface IUserTextSurveyQuestionAnswerRepository : IRepository<UserTextSurveyQuestionAnswer> { }
    public class UserTextSurveyQuestionAnswerRepository : BaseRepository<UserTextSurveyQuestionAnswer>, IUserTextSurveyQuestionAnswerRepository
    {
        public UserTextSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
