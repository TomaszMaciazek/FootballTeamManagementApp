using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserTestResultRepository : IRepository<UserTestResult>
    {
        IQueryable<UserTestResult> GetAllEager();

        IEnumerable<UserTestResult> GetAllFromTest(Guid testId);
    }

    public class UserTestResultRepository : BaseRepository<UserTestResult>, IUserTestResultRepository
    {
        public UserTestResultRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserTestResult> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Test).ThenInclude(x => x.BoolTestQuestions)
            .Include(x => x.Test).ThenInclude(x => x.OptionsTestQuestions)
            .Include(x => x.User);

        public IEnumerable<UserTestResult> GetAllFromTest(Guid testId) => _dbSet
            .AsNoTracking()
            .Include(x => x.Test).ThenInclude(x => x.BoolTestQuestions)
            .Include(x => x.Test).ThenInclude(x => x.OptionsTestQuestions)
            .Include(x => x.User)
            .Where(x => x.Test.Id == testId)
            .ToList();
    }
}
