using App.DataAccess.Interfaces;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{ 

    public interface IUserTextSurveyQuestionAnswerRepository : IRepository<UserTextSurveyQuestionAnswer> {
        IQueryable<UserTextSurveyQuestionAnswer> GetAllEager();
    }
    public class UserTextSurveyQuestionAnswerRepository : BaseRepository<UserTextSurveyQuestionAnswer>, IUserTextSurveyQuestionAnswerRepository
    {
        public UserTextSurveyQuestionAnswerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserTextSurveyQuestionAnswer> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Question)
            .Include(x => x.User);
    }
}
