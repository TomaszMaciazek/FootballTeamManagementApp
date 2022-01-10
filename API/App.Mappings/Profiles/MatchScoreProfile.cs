using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class MatchScoreProfile : Profile
    {
        public MatchScoreProfile()
        {
            CreateMap<MatchPlayerScore, MatchScoreDto>();
            CreateMap<MatchPlayerScore, SimpleMatchScoreDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Player.Id));
            CreateMap<CreateMatchScoreVM, MatchPlayerScore>()
                .ForMember(dest => dest.Match, opt => opt.MapFrom<CreateMatchScoreMatchResolver>())
                .ForMember(dest => dest.Player, opt => opt.MapFrom<CreateMatchScorePlayerResolver>());
        }
    }
}
