using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Resolvers
{
    public class CreateTrainingScorePlayerResolver : IValueResolver<CreateTrainingScoreVM, TrainingScore, Player>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTrainingScorePlayerResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Player Resolve(CreateTrainingScoreVM source, TrainingScore destination, Player destMember, ResolutionContext context)
        {
            return _dbContext.Players.Find(source.PlayerId);
        }
    }
}
