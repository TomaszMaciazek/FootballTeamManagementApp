using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITextSurveyQuestionTemplateRepository : IRepository<TextSurveyQuestionTemplate>{
        IQueryable<TextSurveyQuestionTemplate> GetAllEager();
    }

    public class TextSurveyQuestionTemplateRepository : BaseRepository<TextSurveyQuestionTemplate>, ITextSurveyQuestionTemplateRepository
    {
        public TextSurveyQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<TextSurveyQuestionTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Survey);
    }
}
