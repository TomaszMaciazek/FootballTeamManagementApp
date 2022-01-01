using App.Model.Dtos.History;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class TeamHistoryProfile : Profile
    {
        public TeamHistoryProfile()
        {
            CreateMap<CoachAssignedToTeamEvent, TeamHistoryCoachAssignmentEventDto>();

            CreateMap<PlayerJoinedTeamEvent, TeamHistoryPlayerJoinedTeamEventDto>()
                .ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.PlayerHistory.Player));

            CreateMap<PlayerLeftTeamEvent, TeamHistoryPlayerLeftTeamEventDto>()
                .ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.PlayerHistory.Player));

            CreateMap<TeamPlayersPlayedMatchEvent, TeamHistoryMatchEventDto>();
        }
    }
}
