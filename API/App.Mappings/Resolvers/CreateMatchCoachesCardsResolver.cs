using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateMatchCoachesCardsResolver : IValueResolver<CreateMatchVM, Match, ICollection<CoachCard>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchCoachesCardsResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<CoachCard> Resolve(CreateMatchVM source, Match destination, ICollection<CoachCard> destMember, ResolutionContext context)
        {
            var cards = new List<CoachCard>();

            var coachesIds = source.CoachesCards.Select(x => x.CoachId);

            var coaches = _dbContext.Coaches.Where(x => coachesIds.Any(y => y == x.Id)).ToList();

            foreach (var command in source.CoachesCards)
            {
                var coach = coaches.SingleOrDefault(x => x.Id == command.CoachId);
                cards.Add(new CoachCard
                {
                    Color = command.Color,
                    Coach = coach,
                    Count = command.Count
                });
            }

            return cards;
        }
    }
}
