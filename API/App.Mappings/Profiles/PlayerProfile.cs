﻿using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
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
            CreateMap<Player, SimpleSelectPlayerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(
                    src => $"{src.User.Surname} {src.User.Name}"
                    + (!string.IsNullOrEmpty(src.User.MiddleName) ? $" {src.User.MiddleName}" : "")
                    )
                )
                .ForMember(dest => dest.YearOfBirth, opt => opt.MapFrom(src => src.BirthDate.Year));

            CreateMap<Player, SimplePlayerNameDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname));

            CreateMap<MatchPlayerPerformance, SimpleMatchPlayerDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Player.User.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Player.User.MiddleName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Player.User.Surname))
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team));

            CreateMap<Player, SelectUserPlayerDetailsDto>()
                .ForMember(dest => dest.YearOfBirth, opt => opt.MapFrom(src => src.BirthDate.Year))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Code));

            CreateMap<Player, PlayerAccountDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Code))
                .ForMember(dest => dest.MatchCount, opt => opt.MapFrom(src => src.Matches != null ? src.Matches.Count : 0));

            CreateMap<Player, PlayerListItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname))
                .ForMember(dest => dest.IsStillPlaying, opt => opt.MapFrom(src => !src.FinishedPlaying.HasValue))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Code))
                .ForMember(dest => dest.YearOfBirth, opt => opt.MapFrom(src => src.BirthDate.Year));

            CreateMap<Player, SimplePlayerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(
                    src => $"{src.User.Surname} {src.User.Name}"
                    + (!string.IsNullOrEmpty(src.User.MiddleName) ? $" {src.User.MiddleName}" : "")
                    )
                )
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Code))
                .ForMember(dest => dest.YearOfBirth, opt => opt.MapFrom(src => src.BirthDate.Year));

            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Code))
                .ForMember(dest => dest.MatchCount, opt => opt.MapFrom(src => src.Matches != null ? src.Matches.Count : 0));

            CreateMap<CreatePlayerVM, Player>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom<PlayerCountryResolver>())
                .ForMember(dest => dest.Team, opt => opt.MapFrom<PlayerTeamResolver>())
                .ForMember(dest => dest.User, opt => opt.MapFrom<CreatePlayerUserResolver>())
                .ForMember(dest => dest.FinishedPlaying, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.History, opt => opt.MapFrom(src => new PlayerHistory()));

            CreateMap<UpdatePlayerVM, Player>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom<PlayerTeamResolver>());

            CreateMap<PaginatedList<Player>, PaginatedList<PlayerDto>>();
        }
    }
}
