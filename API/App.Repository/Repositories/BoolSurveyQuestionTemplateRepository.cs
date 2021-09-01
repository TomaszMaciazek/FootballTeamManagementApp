using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IBoolSurveyQuestionTemplateRepository : IRepository<BoolSurveyQuestionTemplate>
    {
        IQueryable<BoolSurveyQuestionTemplate> GetAllEager();
    }

    public class BoolSurveyQuestionTemplateRepository : BaseRepository<BoolSurveyQuestionTemplate>, IBoolSurveyQuestionTemplateRepository
    {
        public BoolSurveyQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<BoolSurveyQuestionTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Survey);
    }
}
