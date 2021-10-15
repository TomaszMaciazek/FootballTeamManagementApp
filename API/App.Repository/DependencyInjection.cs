using App.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();

            service.AddScoped<IPlayerCardRepository, PlayerCardRepository>();
            service.AddScoped<ICoachCardRepository, CoachCardRepository>();
            service.AddScoped<ICoachRepository, CoachRepository>();
            service.AddScoped<IGroupChatRepository, GroupChatRepository>();
            service.AddScoped<IGroupChatImageRepository, GroupChatImageRepository>();
            service.AddScoped<IGroupMessageRepository, GroupMessageRepository>();
            service.AddScoped<IIndividualChatRepository, IndividualChatRepository>();
            service.AddScoped<IIndividualMessageRepository, IndividualMessageRepository>();
            service.AddScoped<IMatchRepository, MatchRepository>();
            service.AddScoped<IMatchPlayerRepository, MatchPlayerRepository>();
            service.AddScoped<IMatchPointRepository, MatchPointRepository>();
            service.AddScoped<IPlayerRepository, PlayerRepository>();
            
            service.AddScoped<ISurveyTemplateRepository, SurveyTemplateRepository>();
            service.AddScoped<IBoolSurveyQuestionTemplateRepository, BoolSurveyQuestionTemplateRepository>();
            service.AddScoped<IOptionsSurveyQuestionTemplateRepository, OptionsSurveyQuestionTemplateRepository>();
            service.AddScoped<IRatingSurveyQuestionTemplateRepository, RatingSurveyQuestionTemplateRepository>();
            service.AddScoped<ITextSurveyQuestionTemplateRepository, TextSurveyQuestionTemplateRepository>();
            service.AddScoped<ISurveyOptionQuestionAnswerTemplateRepository, SurveyOptionQuestionAnswerTemplateRepository>();

            service.AddScoped<ITestTemplateRepository, TestTemplateRepository>();
            service.AddScoped<IBoolTestQuestionTemplateRepository, BoolTestQuestionTemplateRepository>();
            service.AddScoped<IOptionsTestQuestionTemplateRepository, OptionsTestQuestionTemplateRepository>();
            service.AddScoped<ITestOptionQuestionAnswerTemplateRepository, TestOptionQuestionAnswerTemplateRepository>();

            service.AddScoped<ITeamRepository, TeamRepository>();
            service.AddScoped<ITrainingRepository, TrainingRepository>();
            service.AddScoped<ITrainingScoreRepository, TrainingScoreRepository>();
            service.AddScoped<IUserImageRepository, UserImageRepository>();
            
            service.AddScoped<IUserSurveyResultRepository, UserSurveyResultRepository>();
            service.AddScoped<IUserTestResultRepository, UserTestResultRepository>();

            service.AddScoped<IUserBoolSurveyQuestionAnswerRepository, UserBoolSurveyQuestionAnswerRepository>();
            service.AddScoped<IUserOptionsSurveyQuestionAnswerRepository, UserOptionsSurveyQuestionAnswerRepository>();
            service.AddScoped<IUserRatingSurveyQuestionAnswerRepository, UserRatingSurveyQuestionAnswerRepository>();
            service.AddScoped<IUserTextSurveyQuestionAnswerRepository, UserTextSurveyQuestionAnswerRepository>();

            service.AddScoped<IUserBoolTestQuestionAnswerRepository, UserBoolTestQuestionAnswerRepository>();
            service.AddScoped<IUserOptionsTestQuestionAnswerRepository, UserOptionsTestQuestionAnswerRepository>();

            service.AddScoped<IRoleRepository, RoleRepository>();

            service.AddScoped<ILanguageRepository, LanguageRepository>();
            service.AddScoped<ITranslationRepository, TranslationRepository>();
            service.AddScoped<ICountryRepository, CountryRepository>();

            return service;
        }
    }
}
