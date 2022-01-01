using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IPlayerHistoryRepository
    {
        IQueryable<MatchPlayerPerformance> GetMatchPlayerPerformances();
        IQueryable<PlayerJoinedTeamEvent> GetPlayerJoinedTeamEvents();
        IQueryable<PlayerLeftTeamEvent> GetPlayerLeftTeamEvents();
    }

    public class PlayerHistoryRepository : IPlayerHistoryRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public PlayerHistoryRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<MatchPlayerPerformance> GetMatchPlayerPerformances() =>
            _dbContext.MatchPlayersPerformances.AsNoTracking()
            .Include(x => x.PlayerHistory)
            .Include(x => x.Match);

        public IQueryable<PlayerJoinedTeamEvent> GetPlayerJoinedTeamEvents() =>
            _dbContext.PlayerJoinedTeamEvents.AsNoTracking()
            .Include(x => x.PlayerHistory)
            .Include(x => x.TeamHistory).ThenInclude(x => x.Team);

        public IQueryable<PlayerLeftTeamEvent> GetPlayerLeftTeamEvents() =>
            _dbContext.PlayerLeftTeamEvents.AsNoTracking()
            .Include(x => x.PlayerHistory)
            .Include(x => x.TeamHistory).ThenInclude(x => x.Team);
    }
}
