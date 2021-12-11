using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class TrainingScoreProfile : Profile
    {
        public TrainingScoreProfile()
        {
            CreateMap<TrainingScore, TrainingScoreDto>();
            CreateMap<CreateTrainingScoreVM, TrainingScore>()
                .ForMember(dest => dest.Training, opt => opt.MapFrom<CreateTrainingScoreTrainingResolver>())
                .ForMember(dest => dest.Player, opt => opt.MapFrom<CreateTrainingScorePlayerResolver>());
        }
    }
}
