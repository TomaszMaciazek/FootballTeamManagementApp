using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.AnswersTemplates;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITestOptionQuestionAnswerTemplateRepository : IRepository<TestOptionQuestionAnswerTemplate> {
        IEnumerable<TestOptionQuestionAnswerTemplate> GetAllFromQuestion(Guid questionId);
    }

    public class TestOptionQuestionAnswerTemplateRepository : BaseRepository<TestOptionQuestionAnswerTemplate>, ITestOptionQuestionAnswerTemplateRepository
    {
        public TestOptionQuestionAnswerTemplateRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<TestOptionQuestionAnswerTemplate> GetAllFromQuestion(Guid questionId) => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Where(x => x.Question.Id == questionId)
            .ToList();
    }
}
