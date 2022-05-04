using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class TrainingScoreProfile : Profile
    {
        public TrainingScoreProfile()
        {
            CreateMap<TrainingScore, TrainingScoreDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(
                    src => $"{src.Player.User.Surname} {src.Player.User.Name}"
                    + (!string.IsNullOrEmpty(src.Player.User.MiddleName) ? $" {src.Player.User.MiddleName}" : "")
                    )
                );
            CreateMap<CreateTrainingScoreVM, TrainingScore>()
                .ForMember(dest => dest.Training, opt => opt.MapFrom<CreateTrainingScoreTrainingResolver>())
                .ForMember(dest => dest.Player, opt => opt.MapFrom<CreateTrainingScorePlayerResolver>());
            CreateMap<TrainingScore, TrainingScoreListItemDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(
                    src => $"{src.Player.User.Surname} {src.Player.User.Name}"
                    + (!string.IsNullOrEmpty(src.Player.User.MiddleName) ? $" {src.Player.User.MiddleName}" : "")
                    )
                );
        }
    }
}
