using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.UserMiddleware.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace App.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
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
        }
    }
}
