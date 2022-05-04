using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateMatchPlayerPerformancesResolver : IValueResolver<CreateMatchVM, Match, ICollection<MatchPlayerPerformance>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchPlayerPerformancesResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<MatchPlayerPerformance> Resolve(CreateMatchVM source, Match destination, ICollection<MatchPlayerPerformance> destMember, ResolutionContext context)
        {
            var performances = new List<MatchPlayerPerformance>();
            var playersIds = source.PlayerPerformances.Select(x => x.PlayerId);
            var players = _dbContext.Players.Include(x => x.History).Where(x => playersIds.Any(y => y == x.Id)).ToList();
            foreach (var command in source.PlayerPerformances)
            {
                var player = players.SingleOrDefault(x => x.Id == command.PlayerId);
                performances.Add(new MatchPlayerPerformance
                {
                    PlayerPosition = command.PlayerPosition,
                    Player = player,
                    PlayerHistory = player.History
                });
            }

            return performances;
        }
    }
}
