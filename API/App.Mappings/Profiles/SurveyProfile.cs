using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.Entities.SurveyEntities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Profiles
{
    public class SurveyProfile : Profile
    {
        public SurveyProfile()
        {
            CreateMap<CreateSurveyQuestionOptionVM, SurveyQuestionOption>();

            CreateMap<CreateSurveyQuestionVM, SurveyQuestion>();

            CreateMap<CreateSurveyVM, SurveyTemplate>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom<CreateSurveyAuthorResolver>())
                .ForMember(dest => dest.RespondentsResults, opt => opt.MapFrom<CreateSurveyRespondentsResolver>());

            CreateMap<SurveyQuestionOption, SurveyQuestionOptionDto>();

            CreateMap<SurveyQuestion, SurveyQuestionDto>();

            CreateMap<SurveyQuestion, SurveyQuestionWithAnswersDto>();

            CreateMap<SurveyTemplate, FillSurveyDto>();

            CreateMap<SurveyTemplate, SurveyWithResultsDto>();

            CreateMap<UserSurveyResult, UserSurveyResultListItemDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Survey.Author))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Survey.Title))
                .ForMember(dest => dest.SurveyId, opt => opt.MapFrom(src => src.Survey.Id));

            CreateMap<SurveyTextQuestionAnswer, SurveyTextQuestionAnswerDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(x => x.Question.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserResult.User.Id));

            CreateMap<SurveySelectQuestionAnswer, SurveySelectQuestionAnswerDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(x => x.Question.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserResult.User.Id));

            CreateMap<UserSurveyResult, UserSurveyResultDto>();

            CreateMap<UserSurveyResult, SimpleSurveyResultDto>()
                .ForMember(dest => dest.Respondent, opt => opt.MapFrom(src => src.User));

            CreateMap<SurveyTemplate, MySurveyListItemDto>()
                .ForMember(dest => dest.NumberOfCompleatedResults, opt => opt.MapFrom(src => src.RespondentsResults != null ? src.RespondentsResults.Where(x => x.IsCompleated).Count() : 0))
                .ForMember(dest => dest.RespondentsCount, opt => opt.MapFrom(src => src.RespondentsResults != null ? src.RespondentsResults.Count : 0));

            CreateMap<SurveyTemplate, SurveyListItemDto>()
                .ForMember(dest => dest.NumberOfCompleatedResults, opt => opt.MapFrom(src => src.RespondentsResults != null ? src.RespondentsResults.Where(x => x.IsCompleated).Count() : 0))
                .ForMember(dest => dest.RespondentsCount, opt => opt.MapFrom(src => src.RespondentsResults != null ? src.RespondentsResults.Count : 0));

            CreateMap<SurveyTemplate, SimpleSurveyTemplateDto>();
        }
    }
}
