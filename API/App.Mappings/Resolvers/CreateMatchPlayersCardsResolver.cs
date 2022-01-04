using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Mappings.Resolvers
{
    public class CreateMatchPlayersCardsResolver : IValueResolver<CreateMatchVM, Match, ICollection<PlayerCard>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchPlayersCardsResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<PlayerCard> Resolve(CreateMatchVM source, Match destination, ICollection<PlayerCard> destMember, ResolutionContext context)
        {
            var cards = new List<PlayerCard>();

            var playersIds = source.PlayersCards.Select(x => x.PlayerId);

            var players = _dbContext.Players.Include(x => x.Team).Where(x => playersIds.Any(y => y == x.Id)).ToList();

            foreach (var command in source.PlayersCards)
            {
                var player = players.SingleOrDefault(x => x.Id == command.PlayerId);

                cards.Add(new PlayerCard
                {
                    Color = command.Color,
                    Player = player,
                    Count = command.Count,
                    Team = player.Team
                });
            }

            return cards;
        }
    }
}
