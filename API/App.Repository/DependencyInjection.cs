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
            service.AddScoped<IMatchPlayerPerformanceRepository, MatchPlayerPerformanceRepository>();
            service.AddScoped<IMatchPlayerScoreRepository, MatchPlayerScoreRepository>();
            service.AddScoped<IMatchPointRepository, MatchPointRepository>();
            service.AddScoped<IPlayerRepository, PlayerRepository>();
            
            service.AddScoped<ISurveyTemplateRepository, SurveyTemplateRepository>();

            service.AddScoped<ITestTemplateRepository, TestTemplateRepository>();
            service.AddScoped<ITestQuestionRepository, TestQuestionRepository>();

            service.AddScoped<ITeamRepository, TeamRepository>();
            service.AddScoped<ITrainingRepository, TrainingRepository>();
            service.AddScoped<ITrainingScoreRepository, TrainingScoreRepository>();
            
            service.AddScoped<IUserSurveyResultRepository, UserSurveyResultRepository>();
            service.AddScoped<IUserTestResultRepository, UserTestResultRepository>();

            service.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
            service.AddScoped<ISurveyQuestionOptionRepository, SurveyQuestionOptionRepository>();
            service.AddScoped<ISurveySelectQuestionAnswerRepository, SurveySelectQuestionAnswerRepository>();
            service.AddScoped<ISurveyTextQuestionAnswerRepository, SurveyTextQuestionAnswerRepository>();

            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<ICountryRepository, CountryRepository>();

            service.AddScoped<INewsRepository, NewsRepository>();

            service.AddScoped<ITeamHistoryEventsRepository, TeamHistoryEventsRepository>();
            service.AddScoped<IPlayerHistoryRepository, PlayerHistoryRepository>();

            return service;
        }
    }
}
