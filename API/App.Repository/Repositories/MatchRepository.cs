using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IMatchRepository: IRepository<Match>
    {
        Match GetByIdEager(Guid id);
        IQueryable<Match> GetAllEager();
    }

    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Match> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Cards).ThenInclude(x => x.Player)
            .Include(x => x.Points).ThenInclude(x => x.GoalScorer)
            .Include(x => x.Coach)
            .Include(x => x.Players).ThenInclude(x => x.Player);

        public Match GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Cards).ThenInclude(x => x.Player)
            .Include(x => x.Points).ThenInclude(x => x.GoalScorer)
            .Include(x => x.Coach)
            .Include(x => x.Players).ThenInclude(x => x.Player)
            .FirstOrDefault(x => x.Id == id);

        public new void Update(Match entity)
        {
            var matchPlayers = _dbContext.MatchPlayers.Where(x => x.Match.Id == entity.Id);
            var matchPoints = _dbContext.MatchPoints.Where(x => x.Match.Id == entity.Id);
            var matchCards = _dbContext.Cards.Where(x => x.Match.Id == entity.Id);
            _dbContext.MatchPlayers.RemoveRange(matchPlayers);
            _dbContext.MatchPoints.RemoveRange(matchPoints);
            _dbContext.Cards.RemoveRange(matchCards);
            _dbContext.SaveChanges();
            _dbContext.MatchPlayers.AddRange(entity.Players);
            _dbContext.MatchPoints.AddRange(entity.Points);
            _dbContext.Cards.AddRange(entity.Cards);
            base.Update(entity);
        }

        public new void Remove(Guid id)
        {
            var match = _dbContext.Matches
                .AsNoTracking()
                .Include(match => match.Points)
                .Include(match => match.Players)
                .Include(match => match.Cards)
                .FirstOrDefault(match => match.Id == id);
            if (match != null)
            {
                if(match.IsDeleteForbidden)
                {
                    throw new InvalidOperationException("Delete operation is forbiden for this match");
                }
                if (match.Points.Any())
                {
                    _dbContext.MatchPoints.RemoveRange(match.Points);
                }
                if (match.Players.Any())
                {
                    _dbContext.MatchPlayers.RemoveRange(match.Players);
                }
                if (match.Cards.Any())
                {
                    _dbContext.Cards.RemoveRange(match.Cards);
                }
                _dbSet.Remove(match);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }
    }
}
