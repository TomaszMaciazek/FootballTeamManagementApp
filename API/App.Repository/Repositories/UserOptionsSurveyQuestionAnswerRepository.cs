using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IUserOptionsSurveyQuestionAnswerRepository : IRepository<UserOptionsSurveyQuestionAnswer> { }

    public class UserOptionsSurveyQuestionAnswerRepository : BaseRepository<UserOptionsSurveyQuestionAnswer>, IUserOptionsSurveyQuestionAnswerRepository
    {
        public UserOptionsSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
