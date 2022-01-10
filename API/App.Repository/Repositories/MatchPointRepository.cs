using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IMatchPointRepository : IRepository<MatchPoint>
    {
    }
    public class MatchPointRepository : BaseRepository<MatchPoint>, IMatchPointRepository
    {
        public MatchPointRepository(IApplicationDbContext dbContext) : base(dbContext){}
    }
}
