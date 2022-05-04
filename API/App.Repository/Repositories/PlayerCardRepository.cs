using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IPlayerCardRepository : IRepository<PlayerCard> {
        IQueryable<PlayerCard> GetAllEager();
    }
    public class PlayerCardRepository : BaseRepository<PlayerCard>, IPlayerCardRepository
    {
        public PlayerCardRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<PlayerCard> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Player)
            .Include(x => x.Match);
    }
}
