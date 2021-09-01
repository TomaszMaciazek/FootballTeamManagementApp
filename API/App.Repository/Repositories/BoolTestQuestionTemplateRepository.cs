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
        IEnumerable<BoolTestQuestionTemplate> GetAllFromTestTemplate(Guid testTemplateId);
    }

    public class BoolTestQuestionTemplateRepository : BaseRepository<BoolTestQuestionTemplate>, IBoolTestQuestionTemplateRepository
    {
        public BoolTestQuestionTemplateRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IEnumerable<BoolTestQuestionTemplate> GetAllFromTestTemplate(Guid testTemplateId) => _dbSet
            .AsNoTracking()
            .Include(x => x.Test)
            .Where(x => x.Test.Id == testTemplateId)
            .ToList();
    }
}
