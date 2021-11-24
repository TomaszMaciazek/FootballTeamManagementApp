using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.ServiceLayer.Models;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname));

            CreateMap<CreatePlayerVM, Player>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom<PlayerCountryResolver>())
                .ForMember(dest => dest.Team, opt => opt.MapFrom<PlayerTeamResolver>())
                .ForMember(dest => dest.User, opt => opt.MapFrom<CreatePlayerUserResolver>())
                .ForMember(dest => dest.FinishedPlaying, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdatePlayerVM, Player>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom<PlayerTeamResolver>());

            CreateMap<PaginatedList<Player>, PaginatedList<PlayerDto>>();
        }
    }
}
