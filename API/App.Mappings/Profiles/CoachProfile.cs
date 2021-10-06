using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.ServiceLayer.Models;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class CoachProfile : Profile
    {
        public CoachProfile()
        {
            CreateMap<Coach, CoachDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname));

            CreateMap<CreateCoachVM, Coach>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom<CoachCountryResolver>())
                .ForMember(dest => dest.Teams, opt => opt.MapFrom<CoachTeamResolver>())
                .ForMember(dest => dest.User, opt => opt.MapFrom<CoachUserResolver>())
                .ForMember(dest => dest.FinishedWorking, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateCoachVM, Coach>()
                .ForMember(dest => dest.Teams, opt => opt.MapFrom<CoachTeamResolver>());

            CreateMap<PaginatedList<Coach>, PaginatedList<CoachDto>>();
        }
    }
}
