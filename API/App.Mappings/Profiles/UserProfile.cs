using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.UserMiddleware.Helpers;
using AutoMapper;
using System;

namespace App.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, SimpleUserDto>();
            CreateMap<CreateUserVM, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHashHelper.HashPassword(src.Password)))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Email.ToLower()));
            CreateMap<CreateAdminVM, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHashHelper.HashPassword(src.Password)))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.LastPasswordSet, opt => opt.MapFrom(src=>  DateTime.Now))
                .ForMember(dest => dest.BadLogonCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Role, opt => opt.MapFrom< CreateAdminRoleResolver>());

            CreateMap<User, UserAccountDto>()
                .ForMember(dest => dest.Coach, opt => opt.MapFrom(src => src.CoachDetails))
                .ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.PlayerDetails));

            CreateMap<User, SelectUserDto>()
                .ForMember(dest => dest.Coach, opt => opt.MapFrom(src => src.CoachDetails))
                .ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.PlayerDetails));

            CreateMap<User, UserListItemDto>()
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => $"{src.Name} {src.MiddleName}"))
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerDetails != null ? src.PlayerDetails.Id : Guid.Empty))
                .ForMember(dest => dest.CoachId, opt => opt.MapFrom(src => src.CoachDetails != null ? src.CoachDetails.Id : Guid.Empty));
        }
    }
}
