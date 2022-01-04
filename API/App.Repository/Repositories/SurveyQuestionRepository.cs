using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface ISurveyQuestionRepository : IRepository<SurveyQuestion> { }
    public class SurveyQuestionRepository : BaseRepository<SurveyQuestion>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
