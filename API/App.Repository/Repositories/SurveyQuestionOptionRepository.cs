using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface ISurveyQuestionOptionRepository : IRepository<SurveyQuestionOption> { }
    public class SurveyQuestionOptionRepository : BaseRepository<SurveyQuestionOption>, ISurveyQuestionOptionRepository
    {
        public SurveyQuestionOptionRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
