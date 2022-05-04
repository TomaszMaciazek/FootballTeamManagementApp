using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface ISurveySelectQuestionAnswerRepository : IRepository<SurveySelectQuestionAnswer> { }
    public class SurveySelectQuestionAnswerRepository : BaseRepository<SurveySelectQuestionAnswer>, ISurveySelectQuestionAnswerRepository
    {
        public SurveySelectQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
