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
        IQueryable<Match> GetAllEager();
    }

    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Match> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Players).ThenInclude(x => x.Player).ThenInclude(x => x.User)
            .Include(x => x.PlayersCards).ThenInclude(x => x.Player).ThenInclude(x => x.User)
            .Include(x => x.CoachesCards).ThenInclude(x => x.Coach).ThenInclude(x => x.User)
            .Include(x => x.Points).ThenInclude(x => x.GoalScorer).ThenInclude(x => x.User)
            .Include(x => x.Players).ThenInclude(x => x.Player).ThenInclude(x => x.User)
            .Include(x => x.Players).ThenInclude(x => x.Player).ThenInclude(x => x.Team);

        public new void Update(Match entity)
        {
            var matchPlayers = _dbContext.MatchPlayersPerformances.Include(x => x.Match).Where(x => x.Match.Id == entity.Id);
            var matchPoints = _dbContext.MatchPoints.Include(x => x.Match).Where(x => x.Match.Id == entity.Id);
            var playersCards = _dbContext.PlayersCards.Include(x => x.Match).Where(x => x.Match.Id == entity.Id);
            var coachCards = _dbContext.CoachesCards.Include(x => x.Match).Where(x => x.Match.Id == entity.Id);
            _dbContext.MatchPlayersPerformances.RemoveRange(matchPlayers);
            _dbContext.MatchPoints.RemoveRange(matchPoints);
            _dbContext.PlayersCards.RemoveRange(playersCards);
            _dbContext.CoachesCards.RemoveRange(coachCards);
            _dbContext.SaveChanges();
            _dbContext.MatchPlayersPerformances.AddRange(entity.Players);
            _dbContext.MatchPoints.AddRange(entity.Points);
            _dbContext.PlayersCards.AddRange(entity.PlayersCards);
            _dbContext.CoachesCards.AddRange(entity.CoachesCards);
            base.Update(entity);
        }

        public new void Remove(Guid id)
        {
            var match = _dbContext.Matches
                .AsNoTracking()
                .Include(match => match.Points)
                .Include(match => match.Players)
                .Include(match => match.PlayersCards)
                .FirstOrDefault(match => match.Id == id);
            if (match != null)
            {
                if (match.Points.Any())
                {
                    _dbContext.MatchPoints.RemoveRange(match.Points);
                }
                if (match.Players.Any())
                {
                    _dbContext.MatchPlayersPerformances.RemoveRange(match.Players);
                }
                if (match.PlayersCards.Any())
                {
                    _dbContext.PlayersCards.RemoveRange(match.PlayersCards);
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
