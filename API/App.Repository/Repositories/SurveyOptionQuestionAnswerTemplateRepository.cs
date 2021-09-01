using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ISurveyOptionQuestionAnswerTemplateRepository : IRepository<SurveyOptionQuestionAnswerTemplate>
    {
        IQueryable<SurveyOptionQuestionAnswerTemplate> GetAllEager();
    }

    public class SurveyOptionQuestionAnswerTemplateRepository : BaseRepository<SurveyOptionQuestionAnswerTemplate>, ISurveyOptionQuestionAnswerTemplateRepository
    {
        public SurveyOptionQuestionAnswerTemplateRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<SurveyOptionQuestionAnswerTemplate> GetAllEager() => _dbSet
                .AsNoTracking()
                .Include(x => x.Question).ThenInclude(x => x.Survey);
    }
}
