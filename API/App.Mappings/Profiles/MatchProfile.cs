using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, MatchListItemDto>();

            CreateMap<CreateMatchVM, Match>()
                .ForMember(dest => dest.Players, opt => opt.MapFrom<CreateMatchPlayerPerformancesResolver>())
                .ForMember(dest => dest.Points, opt => opt.MapFrom<CreateMatchMatchPointsResolver>())
                .ForMember(dest => dest.PlayersCards, opt => opt.MapFrom<CreateMatchPlayersCardsResolver>())
                .ForMember(dest => dest.CoachesCards, opt => opt.MapFrom<CreateMatchCoachesCardsResolver>())
                .ForMember(dest => dest.TeamPlayersPlayedMatchEvents, opt => opt.MapFrom<CreateMatchTeamPlayersPlayedMatchEventsResolver>());

            CreateMap<Match, SimpleMatchDto>();

            CreateMap<Match, MatchDto>();
        }
    }
}
