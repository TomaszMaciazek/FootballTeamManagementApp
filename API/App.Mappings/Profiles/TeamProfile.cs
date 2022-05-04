using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDto>();

            CreateMap<Team, SimpleTeamDto>();

            CreateMap<Team, TeamListItemDto>()
                .ForMember(dest => dest.MembersCount, opt => opt.MapFrom(src => (src.Players != null) ? src.Players.Count : 0))
                .ForMember(dest => dest.CanBeDeleted, opt => opt.MapFrom(src => src.History.PlayerJoinedTeamEvents.Count == 0));

            CreateMap<CreateTeamVM, Team>()
                .ForMember(dest => dest.MainCoach, opt => opt.MapFrom<TeamCoachResolver>())
                .ForMember(dest => dest.History, opt => opt.MapFrom<CreateTeamHistoryResolver>());
        }
    }
}
