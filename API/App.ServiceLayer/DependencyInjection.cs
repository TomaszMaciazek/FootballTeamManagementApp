﻿using App.ServiceLayer.Services;
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
            services.AddScoped<IMatchPointService, MatchPointService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ISurveyTemplateService, SurveyTemplateService>();
            services.AddScoped<IUserSurveyResultService, UserSurveyResultService>();

            services.AddScoped<ITestTemplateService, TestTemplateService>();
            services.AddScoped<IUserTestResultService, UserTestResultService>();

            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<ITeamHistoryService, TeamHistoryService>();
            services.AddScoped<IPlayerHistoryService, PlayerHistoryService>();

            services.AddScoped<IStatisticsService, StatisticsService>();
            return services;
        }
    }
}
