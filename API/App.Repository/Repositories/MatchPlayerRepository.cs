using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IMatchPlayerRepository : IRepository<MatchPlayer> { }
    public class MatchPlayerRepository : BaseRepository<MatchPlayer>, IMatchPlayerRepository
    {
        public MatchPlayerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
