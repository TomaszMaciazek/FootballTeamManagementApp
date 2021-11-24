using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<CreateNewsVM, News>().ReverseMap();

        }
    }
}
