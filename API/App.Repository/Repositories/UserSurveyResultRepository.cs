using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserSurveyResultRepository : IRepository<UserSurveyResult>
    {
        IQueryable<UserSurveyResult> GetAllEager();
    }

    public class UserSurveyResultRepository : BaseRepository<UserSurveyResult>, IUserSurveyResultRepository
    {
        public UserSurveyResultRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserSurveyResult> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.User);
    }
}
