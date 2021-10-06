using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.UserMiddleware.Helpers;
using AutoMapper;
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
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<Role>()));
        }
    }
}
