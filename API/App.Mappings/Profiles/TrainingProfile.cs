using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<Training, TrainingDto>()
                .ForMember(x => x.TrainingScores, opt => opt.MapFrom(src => src.Scores));
            CreateMap<CreateTrainingVM, Training>();
            CreateMap<Training, TrainingListItem>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(x => x.Date));
        }
    }
}
