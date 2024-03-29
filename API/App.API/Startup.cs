using App.API.Services;
using App.DataAccess;
using App.DataAccess.Interfaces;
using App.Infrastructure;
using App.Mappings;
using App.Repository;
using App.ServiceLayer;
using App.UserMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(provider => provider.GetService<IHttpContextAccessor>().HttpContext?.User);

            services.AddCors();

            services.AddControllers();

            services.AddDataAccess();
            services.AddInfrastructure(Configuration);
            services.AddRepositories();
            services.AddUserMiddleware();
            services.AddServices();
            services.AddMappings();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidAudience = Configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.UsersPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Users));
                options.AddPolicy(Permissions.AdminSettingsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.AdminSettings));

                options.AddPolicy(Permissions.PlayersPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Players));
                options.AddPolicy(Permissions.PlayersTeamsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.PlayersTeamsEdit));
                options.AddPolicy(Permissions.PlayersHistoriesPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.PlayersHistories));

                options.AddPolicy(Permissions.CoachesPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Coaches));
                options.AddPolicy(Permissions.CoachesTeamsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.CoachesTeamsEdit));

                options.AddPolicy(Permissions.TrainingsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Trainings));
                options.AddPolicy(Permissions.TrainingsActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingsActivate));
                options.AddPolicy(Permissions.TrainingsDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingsDeactivate));
                options.AddPolicy(Permissions.TrainingsAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingsAdd));
                options.AddPolicy(Permissions.TrainingsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingsEdit));
                options.AddPolicy(Permissions.TrainingsDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingsDelete));

                options.AddPolicy(Permissions.MatchesPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Matches));
                options.AddPolicy(Permissions.MatchesActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchesActivate));
                options.AddPolicy(Permissions.MatchesDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchesDeactivate));
                options.AddPolicy(Permissions.MatchesAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchesAdd));
                options.AddPolicy(Permissions.MatchesEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchesEdit));
                options.AddPolicy(Permissions.MatchesDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchesDelete));

                options.AddPolicy(Permissions.SurveysPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Surveys));
                options.AddPolicy(Permissions.SurveysAllPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.SurveysAll));
                options.AddPolicy(Permissions.SurveysAssignedToMePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.SurveysAssignedToMe));
                options.AddPolicy(Permissions.SurveysCreatedByMePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.SurveysCreatedByMe));
                options.AddPolicy(Permissions.RespondentsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.GetRespondents));
                options.AddPolicy(Permissions.SurveysAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.SurveysAdd));
                options.AddPolicy(Permissions.SurveysEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.SurveysEdit));
                options.AddPolicy(Permissions.SurveysDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.SurveysDelete));

                options.AddPolicy(Permissions.TestsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Tests));
                options.AddPolicy(Permissions.TestsAllPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsAll));
                options.AddPolicy(Permissions.TestsAssignedToMePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsAssignedToMe));
                options.AddPolicy(Permissions.TestsCreatedByMePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsCreatedByMe));
                options.AddPolicy(Permissions.TestsActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsActivate));
                options.AddPolicy(Permissions.TestsDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsDeactivate));
                options.AddPolicy(Permissions.TestsAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsAdd));
                options.AddPolicy(Permissions.TestsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsEdit));
                options.AddPolicy(Permissions.TestsDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TestsDelete));

                options.AddPolicy(Permissions.MessagesPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Messages));

                options.AddPolicy(Permissions.NewsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.News));
                options.AddPolicy(Permissions.NewsActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.NewsActivate));
                options.AddPolicy(Permissions.NewsDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.NewsDeactivate));
                options.AddPolicy(Permissions.NewsAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.NewsAdd));
                options.AddPolicy(Permissions.NewsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.NewsEdit));
                options.AddPolicy(Permissions.NewsDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.NewsDelete));

                options.AddPolicy(Permissions.TrainingScoresPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingScores));
                options.AddPolicy(Permissions.TrainingScoresActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingScoresActivate));
                options.AddPolicy(Permissions.TrainingScoresDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingScoresDeactivate));
                options.AddPolicy(Permissions.TrainingScoresAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingScoresAdd));
                options.AddPolicy(Permissions.TrainingScoresEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingScoresEdit));
                options.AddPolicy(Permissions.TrainingScoresDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TrainingScoresDelete));

                options.AddPolicy(Permissions.MatchScoresPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchScores));
                options.AddPolicy(Permissions.MatchScoresActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchScoresActivate));
                options.AddPolicy(Permissions.MatchScoresDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchScoresDeactivate));
                options.AddPolicy(Permissions.MatchScoresAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchScoresAdd));
                options.AddPolicy(Permissions.MatchScoresEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchScoresEdit));
                options.AddPolicy(Permissions.MatchScoresDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchScoresDelete));

                options.AddPolicy(Permissions.TeamsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Teams));
                options.AddPolicy(Permissions.TeamsActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TeamsActivate));
                options.AddPolicy(Permissions.TeamsDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TeamsDeactivate));
                options.AddPolicy(Permissions.TeamsAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TeamsAdd));
                options.AddPolicy(Permissions.TeamsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TeamsEdit));
                options.AddPolicy(Permissions.TeamsDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TeamsDelete));
                options.AddPolicy(Permissions.TeamsHistoriesPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.TeamsHistories));

                options.AddPolicy(Permissions.CardsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.Cards));
                options.AddPolicy(Permissions.CardsActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.CardsActivate));
                options.AddPolicy(Permissions.CardsDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.CardsDeactivate));
                options.AddPolicy(Permissions.CardsAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.CardsAdd));
                options.AddPolicy(Permissions.CardsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.CardsEdit));
                options.AddPolicy(Permissions.CardsDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.CardsDelete));

                options.AddPolicy(Permissions.MatchPlayerPerformancesPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPlayerPerformances));
                options.AddPolicy(Permissions.MatchPlayerPerformancesActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPlayerPerformancesActivate));
                options.AddPolicy(Permissions.MatchPlayerPerformancesDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPlayerPerformancesDeactivate));
                options.AddPolicy(Permissions.MatchPlayerPerformancesAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPlayerPerformancesAdd));
                options.AddPolicy(Permissions.MatchPlayerPerformancesEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPlayerPerformancesEdit));
                options.AddPolicy(Permissions.MatchPlayerPerformancesDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPlayerPerformancesDelete));

                options.AddPolicy(Permissions.MatchPointsPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPoints));
                options.AddPolicy(Permissions.MatchPointsActivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPointsActivate));
                options.AddPolicy(Permissions.MatchPointsDeactivatePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPointsDeactivate));
                options.AddPolicy(Permissions.MatchPointsAddPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPointsAdd));
                options.AddPolicy(Permissions.MatchPointsEditPolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPointsEdit));
                options.AddPolicy(Permissions.MatchPointsDeletePolicy, policy => policy.RequireClaim(Permissions.ClaimType, Permissions.MatchPointsDelete));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    Array.Empty<string>()
                }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/Logs");
            loggerFactory.AddFile($"{directoryInfo.FullName}/Log-{DateTime.Now}.log");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
