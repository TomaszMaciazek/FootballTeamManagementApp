using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Resolvers
{
    public class CreateMatchScoreMatchResolver : IValueResolver<CreateMatchScoreVM, MatchPlayerScore, Match>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchScoreMatchResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Match Resolve(CreateMatchScoreVM source, MatchPlayerScore destination, Match destMember, ResolutionContext context)
        {
            return _dbContext.Matches.Find(source.MatchId);
        }
    }
}
