using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IOptionsSurveyQuestionTemplateRepository : IRepository<OptionsSurveyQuestionTemplate>
    {
        IQueryable<OptionsSurveyQuestionTemplate> GetAllEager();
    }

    public class OptionsSurveyQuestionTemplateRepository : BaseRepository<OptionsSurveyQuestionTemplate>, IOptionsSurveyQuestionTemplateRepository
    {
        public OptionsSurveyQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<OptionsSurveyQuestionTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Survey)
            .Include(x => x.AnswerTemplates);
    }
}
