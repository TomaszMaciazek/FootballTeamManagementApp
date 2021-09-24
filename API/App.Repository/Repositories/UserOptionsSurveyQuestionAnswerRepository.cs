using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserOptionsSurveyQuestionAnswerRepository : IRepository<UserOptionsSurveyQuestionAnswer> {
        IQueryable<UserOptionsSurveyQuestionAnswer> GetAllEager();
    }

    public class UserOptionsSurveyQuestionAnswerRepository : BaseRepository<UserOptionsSurveyQuestionAnswer>, IUserOptionsSurveyQuestionAnswerRepository
    {
        public UserOptionsSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserOptionsSurveyQuestionAnswer> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Include(x => x.User);
    }
}
