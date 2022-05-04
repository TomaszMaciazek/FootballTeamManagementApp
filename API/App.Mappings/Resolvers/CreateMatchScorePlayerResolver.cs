using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Resolvers
{
    public class CreateMatchScorePlayerResolver : IValueResolver<CreateMatchScoreVM, MatchPlayerScore, Player>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMatchScorePlayerResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Player Resolve(CreateMatchScoreVM source, MatchPlayerScore destination, Player destMember, ResolutionContext context)
        {
            return _dbContext.Players.Find(source.PlayerId);
        }
    }
}
