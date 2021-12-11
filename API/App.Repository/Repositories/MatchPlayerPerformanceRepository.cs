using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IMatchPlayerPerformanceRepository : IRepository<MatchPlayerPerformance> { }
    public class MatchPlayerPerformanceRepository : BaseRepository<MatchPlayerPerformance>, IMatchPlayerPerformanceRepository
    {
        public MatchPlayerPerformanceRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
