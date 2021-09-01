using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IMatchPointRepository : IRepository<MatchPoint>
    {
        IQueryable<MatchPoint> GetAllEager();
        IEnumerable<MatchPoint> GetAllFromMatch(Guid matchId);
        IEnumerable<MatchPoint> GetAllFromPlayer(Guid playerId);
    }
    public class MatchPointRepository : BaseRepository<MatchPoint>, IMatchPointRepository
    {
        public MatchPointRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<MatchPoint> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Match)
            .Include(x => x.GoalScorer);

        public IEnumerable<MatchPoint> GetAllFromMatch(Guid matchId) => _dbSet
                .AsNoTracking()
                .Include(x => x.Match)
                .Include(x => x.GoalScorer)
                .Where(x => x.Match.Id == matchId)
                .ToList();

        public IEnumerable<MatchPoint> GetAllFromPlayer(Guid playerId) => _dbSet
                .AsNoTracking()
                .Include(x => x.Match)
                .Include(x => x.GoalScorer)
                .Where(x => x.GoalScorer.Id == playerId)
                .ToList();
    }
}
