using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.QuestionTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IBoolTestQuestionTemplateRepository : IRepository<BoolTestQuestionTemplate>
    {
        IQueryable<BoolTestQuestionTemplate> GetAllEager();
    }

    public class BoolTestQuestionTemplateRepository : BaseRepository<BoolTestQuestionTemplate>, IBoolTestQuestionTemplateRepository
    {
        public BoolTestQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<BoolTestQuestionTemplate> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Test).ThenInclude(x => x.OptionsTestQuestions).ThenInclude(x => x.Answers)
            .Include(x => x.Test).ThenInclude(x => x.BoolTestQuestions);
    }
}
