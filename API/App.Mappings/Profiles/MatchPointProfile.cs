using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class MatchPointProfile : Profile
    {
        public MatchPointProfile()
        {
            CreateMap<MatchPoint, MatchPointDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.GoalScorer.Id))
                .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.Match.Id));
        }
    }
}
