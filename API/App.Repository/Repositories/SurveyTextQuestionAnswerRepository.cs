using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface ISurveyTextQuestionAnswerRepository : IRepository<SurveyTextQuestionAnswer> { }
    public class SurveyTextQuestionAnswerRepository : BaseRepository<SurveyTextQuestionAnswer>, ISurveyTextQuestionAnswerRepository
    {
        public SurveyTextQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
