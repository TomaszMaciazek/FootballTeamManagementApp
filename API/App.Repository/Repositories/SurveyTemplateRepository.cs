using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ISurveyTemplateRepository : IRepository<SurveyTemplate>
    {
        IQueryable<SurveyTemplate> GetAllEager();

        SurveyTemplate GetByIdEager(Guid id);
    }

    public class SurveyTemplateRepository : BaseRepository<SurveyTemplate>, ISurveyTemplateRepository
    {
        public SurveyTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<SurveyTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.BoolQuestionTemplates)
            .Include(x => x.OptionsQuestionTemplates).ThenInclude(x => x.AnswerTemplates)
            .Include(x => x.RatingQuestionTemplates)
            .Include(x => x.TextQuestionTemplates)
            .Include(x => x.RespondentsResults).ThenInclude(x => x.User);

        public SurveyTemplate GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.BoolQuestionTemplates)
            .Include(x => x.OptionsQuestionTemplates).ThenInclude(x => x.AnswerTemplates)
            .Include(x => x.RatingQuestionTemplates)
            .Include(x => x.TextQuestionTemplates)
            .Include(x => x.RespondentsResults).ThenInclude(x => x.User)
            .FirstOrDefault(x => x.Id == id);
    }
}
