using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITeamHistoryEventsRepository
    {
        IQueryable<CoachAssignedToTeamEvent> GetCoachAssignments();
        IQueryable<TeamPlayersPlayedMatchEvent> GetMatchesPlayed();
        IQueryable<PlayerJoinedTeamEvent> GetPlayerJoinedEvents();
        IQueryable<PlayerLeftTeamEvent> GetPlayerLeftEvents();
    }

    public class TeamHistoryEventsRepository : ITeamHistoryEventsRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public TeamHistoryEventsRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<CoachAssignedToTeamEvent> GetCoachAssignments()
            => _dbContext.CoachAssignedToTeamEvents.AsNoTracking()
            .Include(x => x.Coach).ThenInclude(x => x.User)
            .Include(x => x.TeamHistory);

        public IQueryable<TeamPlayersPlayedMatchEvent> GetMatchesPlayed()
            => _dbContext.TeamPlayersPlayedMatchEvents.AsNoTracking()
            .Include(x => x.Match)
            .Include(x => x.TeamHistory);

        public IQueryable<PlayerJoinedTeamEvent> GetPlayerJoinedEvents()
            => _dbContext.PlayerJoinedTeamEvents.AsNoTracking()
            .Include(x => x.PlayerHistory).ThenInclude(x => x.Player).ThenInclude(x => x.User)
            .Include(x => x.TeamHistory);

        public IQueryable<PlayerLeftTeamEvent> GetPlayerLeftEvents()
            => _dbContext.PlayerLeftTeamEvents.AsNoTracking()
            .Include(x => x.PlayerHistory).ThenInclude(x => x.Player).ThenInclude(x => x.User)
            .Include(x => x.TeamHistory);
    }
}
