using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Resolvers
{
    public class CreateTrainingScoreTrainingResolver : IValueResolver<CreateTrainingScoreVM, TrainingScore, Training>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTrainingScoreTrainingResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Training Resolve(CreateTrainingScoreVM source, TrainingScore destination, Training destMember, ResolutionContext context)
        {
            return _dbContext.Trainings.Find(source.TrainingId);
        }
    }
}
