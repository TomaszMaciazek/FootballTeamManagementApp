using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IRatingSurveyQuestionTemplateRepository : IRepository<RatingSurveyQuestionTemplate>
    {
        IQueryable<RatingSurveyQuestionTemplate> GetAllEager();
    }

    public class RatingSurveyQuestionTemplateRepository : BaseRepository<RatingSurveyQuestionTemplate>, IRatingSurveyQuestionTemplateRepository
    {
        public RatingSurveyQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<RatingSurveyQuestionTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Survey);
    }
}
