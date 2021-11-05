using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>().ReverseMap();

        }
    }
}
