﻿using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class TranslationProfile : Profile
    {
        public TranslationProfile()
        {
            CreateMap<Translation, TranslationDto>()
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.Language.Id));
        }
    }
}
