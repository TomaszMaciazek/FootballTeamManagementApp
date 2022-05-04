using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class CoachCardProfile : Profile
    {
        public CoachCardProfile()
        {
            CreateMap<CoachCard, CoachCardDto>()
                .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.Match.Id));
        }
    }
}
