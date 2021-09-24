using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.QuestionTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IOptionsTestQuestionTemplateRepository : IRepository<OptionsTestQuestionTemplate>
    {
        IQueryable<OptionsTestQuestionTemplate> GetAllEager();
    }

    public class OptionsTestQuestionTemplateRepository : BaseRepository<OptionsTestQuestionTemplate>, IOptionsTestQuestionTemplateRepository
    {
        public OptionsTestQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<OptionsTestQuestionTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Test).ThenInclude(x => x.OptionsTestQuestions)
            .Include(x => x.UserAnswers).ThenInclude(x => x.User);
    }
}
