using AutoMapper;
using System;

namespace App.Mappings.Profiles
{
    public class TypesProfile : Profile
    {
        public TypesProfile()
        {
            CreateMap<Guid?, Guid>().ConvertUsing((src, dest) => src ?? Guid.Empty);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? false);
        }
    }
}
