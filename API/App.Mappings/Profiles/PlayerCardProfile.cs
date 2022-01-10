using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class PlayerCardProfile : Profile
    {
        public PlayerCardProfile()
        {
            CreateMap<PlayerCard, PlayerCardDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.Match.Id));
        }
    }
}
