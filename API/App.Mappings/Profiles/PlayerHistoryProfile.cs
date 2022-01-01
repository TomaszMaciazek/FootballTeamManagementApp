using App.Model.Dtos.History;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class PlayerHistoryProfile : Profile
    {
        public PlayerHistoryProfile()
        {
            CreateMap<MatchPlayerPerformance, PlayerHistoryMatchDto>();

            CreateMap<PlayerJoinedTeamEvent, PlayerHistoryPlayerJoinedTeamEventDto>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.TeamHistory.Team));

            CreateMap<PlayerLeftTeamEvent, PlayerHistoryPlayerLeftTeamEventDto>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.TeamHistory.Team));

        }
    }
}
