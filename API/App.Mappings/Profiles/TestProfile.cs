using App.Mappings.Resolvers;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities.TestEntities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<CreateTestQuestionOptionVM, TestQuestionOption>();
            CreateMap<CreateTestQuestionVM, TestQuestion>();

            CreateMap<CreateTestVM, TestTemplate>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom<CreateTestAuthorResolver>())
                .ForMember(dest => dest.UserResults, opt => opt.MapFrom<CreateTestRespondentsResolver>());

            CreateMap<TestTemplate, TestListItemDto>()
                .ForMember(dest => dest.NumberOfCompleatedResults, opt => opt.MapFrom(src => src.UserResults != null ? src.UserResults.Where(x => x.IsCompleated).Count() : 0))
                .ForMember(dest => dest.RespondentsCount, opt => opt.MapFrom(src => src.UserResults != null ? src.UserResults.Count : 0));

            CreateMap<TestTemplate, MyTestListItemDto>()
                .ForMember(dest => dest.NumberOfCompleatedResults, opt => opt.MapFrom(src => src.UserResults != null ? src.UserResults.Where(x => x.IsCompleated).Count() : 0))
                .ForMember(dest => dest.RespondentsCount, opt => opt.MapFrom(src => src.UserResults != null ? src.UserResults.Count : 0));

            CreateMap<UserTestResult, UserTestResultListItemDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Test.Author))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Test.Title))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.Test.Id));

            CreateMap<TestQuestionOption, FillTestQuestionOptionDto>();

            CreateMap<TestQuestion, FillTestQuestionDto>()
                .ForMember(dest => dest.PointsToScore, opt => opt.MapFrom(src => src.Options != null ? src.Options.Where(x => x.Points > 0).Sum(x => x.Points) : 0));

            CreateMap<TestTemplate, FillTestDto>();

            CreateMap<TestTemplate, TestTemplateDto>();

            CreateMap<UserTestResult, UserTestResultDto>();

            CreateMap<TestQuestionAnswer, TestQuestionAnswerDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(x => x.Question.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserResult.User.Id));

            CreateMap<TestTemplate, SimpleTestTemplateDto>();

            CreateMap<TestQuestionOption, TestQuestionOptionDto>();

            CreateMap<TestQuestion, TestQuestionDto>();

            CreateMap<UserTestResult, SimpleUserTestResultListItemDto>()
                .ForMember(dest => dest.Respondent, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.Test.Id));
        }
    }
}
