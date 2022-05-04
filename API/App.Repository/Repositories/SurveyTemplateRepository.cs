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
    }

    public class SurveyTemplateRepository : BaseRepository<SurveyTemplate>, ISurveyTemplateRepository
    {
        public SurveyTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}


        public new void Remove(Guid id)
        {
            var survey = _dbContext.SurveyTemplates
                .AsNoTracking()
                .Include(x => x.RespondentsResults)
                .SingleOrDefault(x => x.Id == id);
            if (survey != null)
            {
                if (survey.RespondentsResults.Any())
                {
                    _dbContext.UsersSurveyResults.RemoveRange(survey.RespondentsResults);
                }
                _dbSet.Remove(survey);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }
    }
}
