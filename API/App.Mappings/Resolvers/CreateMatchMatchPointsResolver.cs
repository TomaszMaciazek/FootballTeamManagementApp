using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateMatchMatchPointsResolver : IValueResolver<CreateMatchVM, Match, ICollection<MatchPoint>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchMatchPointsResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<MatchPoint> Resolve(CreateMatchVM source, Match destination, ICollection<MatchPoint> destMember, ResolutionContext context)
        {
            var points = new List<MatchPoint>();
            var playersIds = source.MatchPoints.Select(x => x.PlayerId);
            var players = _dbContext.Players.Include(x => x.Team).Where(x => playersIds.Any(y => y == x.Id)).ToList();
            foreach (var command in source.MatchPoints)
            {
                var player = players.SingleOrDefault(x => x.Id == command.PlayerId);
                points.Add(new MatchPoint
                {
                    Point = command.Point,
                    GoalScorer = player,
                    MinuteOfMatch = command.MinuteOfMatch,
                    Team = player.Team
                });
            }

            return points;
        }
    }
}
