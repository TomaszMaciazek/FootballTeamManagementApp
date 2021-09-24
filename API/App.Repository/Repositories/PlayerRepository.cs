using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        IQueryable<Player> GetAllEager();
        IQueryable<Player> GetByIdEager(Guid id);
        IQueryable<Player> GetByUserIdEager(Guid userId);
    }

    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Player> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Matches)
            .Include(x => x.Team)
            .Include(x => x.User)
            .Include(x => x.TrainingScores).ThenInclude(x => x.Training)
            .Include(x => x.Cards).ThenInclude(x => x.Match)
            .Include(x => x.MatchPoints).ThenInclude(x => x.Match);

        public IQueryable<Player> GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Matches)
            .Include(x => x.Team)
            .Include(x => x.User)
            .Include(x => x.TrainingScores).ThenInclude(x => x.Training)
            .Include(x => x.Cards).ThenInclude(x => x.Match)
            .Include(x => x.MatchPoints).ThenInclude(x => x.Match)
            .Where(x => x.Id == id);

        public IQueryable<Player> GetByUserIdEager(Guid userId) => _dbSet
            .AsNoTracking()
            .Include(x => x.Matches)
            .Include(x => x.Team)
            .Include(x => x.User)
            .Include(x => x.TrainingScores).ThenInclude(x => x.Training)
            .Include(x => x.Cards).ThenInclude(x => x.Match)
            .Include(x => x.MatchPoints).ThenInclude(x => x.Match)
            .Where(x => x.UserId == userId);
    }
}
