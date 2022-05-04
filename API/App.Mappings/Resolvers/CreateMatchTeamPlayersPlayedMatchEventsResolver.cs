using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateMatchTeamPlayersPlayedMatchEventsResolver : IValueResolver<CreateMatchVM, Match, ICollection<TeamPlayersPlayedMatchEvent>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchTeamPlayersPlayedMatchEventsResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<TeamPlayersPlayedMatchEvent> Resolve(CreateMatchVM source, Match destination, ICollection<TeamPlayersPlayedMatchEvent> destMember, ResolutionContext context)
        {
            var events = new List<TeamPlayersPlayedMatchEvent>();

            var playersIds = source.PlayerPerformances.Select(x => x.PlayerId);
            var teams = _dbContext.Players.Include(x => x.Team).ThenInclude(x => x.History).Where(x => playersIds.Any(y => y == x.Id) && x.Team != null).Select(x => x.Team).ToList();

            var teamsDistinct = teams.GroupBy(x => x.Id).Select(g => g.First());

            foreach(var team in teamsDistinct)
            {
                events.Add(new TeamPlayersPlayedMatchEvent
                {
                    TeamHistory = team.History
                });
            }

            return events;
        }
    }
}
