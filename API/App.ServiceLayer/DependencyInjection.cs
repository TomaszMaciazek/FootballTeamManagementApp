using App.ServiceLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ServiceLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPlayerCardService, PlayerCardService>();
            services.AddScoped<ICoachCardService, CoachCardService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchPlayerPerformanceService, MatchPlayerPerformanceService>();
            services.AddScoped<IMatchPlayerScoreService, MatchPlayerScoreService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITrainingScoreService, TrainingScoreService>();

            services.AddScoped<IGroupChatService, GroupChatService>();
            services.AddScoped<IIndividualChatService, IndividualChatService>();
            services.AddScoped<IIndividualMessageService, IndividualMessageService>();
            services.AddScoped<IGroupMessageService, GroupMessageService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ISurveyTemplateService, SurveyTemplateService>();
            services.AddScoped<IBoolSurveyQuestionTemplateService, BoolSurveyQuestionTemplateService>();
            services.AddScoped<ITextSurveyQuestionTemplateService, TextSurveyQuestionTemplateService>();
            services.AddScoped<IRatingSurveyQuestionTemplateService, RatingSurveyQuestionTemplateService>();
            services.AddScoped<IOptionsSurveyQuestionTemplateService, OptionsSurveyQuestionTemplateService>();
            services.AddScoped<IUserBoolSurveyQuestionAnswerService, UserBoolSurveyQuestionAnswerService>();
            services.AddScoped<IUserRatingSurveyQuestionAnswerService, UserRatingSurveyQuestionAnswerService>();
            services.AddScoped<IUserOptionsSurveyQuestionAnswerService, UserOptionsSurveyQuestionAnswerService>();
            services.AddScoped<IUserTextSurveyQuestionAnswerService, UserTextSurveyQuestionAnswerService>();
            services.AddScoped<IUserSurveyResultService, UserSurveyResultService>();

            services.AddScoped<ITestTemplateService, TestTemplateService>();
            services.AddScoped<IBoolTestQuestionTemplateService, BoolTestQuestionTemplateService>();
            services.AddScoped<IOptionsTestQuestionTemplateService, OptionsTestQuestionTemplateService>();
            services.AddScoped<IUserBoolTestQuestionAnswerService, UserBoolTestQuestionAnswerService>();
            services.AddScoped<IUserOptionsTestQuestionAnswerService, UserOptionsTestQuestionAnswerService>();
            services.AddScoped<IGroupChatImageService, GroupChatImageService>();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<INewsService, NewsService>();
            return services;
        }
    }
}
