using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
        }
    }
}
