using App.Model.Entities;
using App.Model.Entities.SurveyEntities;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Entities.SurveyEntities.AnswersTemplates;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using App.Model.Entities.TestEntities;
using App.Model.Entities.TestEntities.AnswersResults;
using App.Model.Entities.TestEntities.AnswersTemplates;
using App.Model.Entities.TestEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        #region Match Entities
        public DbSet<Card> Cards { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchPoint> MatchPoints { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        #endregion

        #region Chat Entities
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<GroupChatImage> GroupChatImages { get; set; }
        public DbSet<IndividualChat> IndividualChats { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<IndividualMessage> IndividualMessages { get; set; }
        #endregion

        #region User Entites
        public DbSet<User> Users { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        #endregion

        #region Training Entities
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingScore> TrainingScores { get; set; }
        public DbSet<Team> Teams { get; set; }
        #endregion

        #region Survey Entites
        public DbSet<SurveyTemplate> SurveyTemplates { get; set; }
        public DbSet<TextSurveyQuestionTemplate> TextSurveyQuestionsTemplates { get; set; }
        public DbSet<BoolSurveyQuestionTemplate> BoolSurveyQuestionsTemplates { get; set; }
        public DbSet<OptionsSurveyQuestionTemplate> OptionsSurveyQuestionsTemplates { get; set; }
        public DbSet<RatingSurveyQuestionTemplate> RatingSurveyQuestionsTemplates { get; set; }
        public DbSet<SurveyOptionQuestionAnswerTemplate> SurveyOptionQuestionsAnswerTemplates { get; set; }
        public DbSet<UserBoolSurveyQuestionAnswer> UserBoolSurveyQuestionsAnswers { get; set; }
        public DbSet<UserOptionsSurveyQuestionAnswer> UserOptionsSurveyQuestionsAnswers { get; set; }
        public DbSet<UserTextSurveyQuestionAnswer> UserTextSurveyQuestionsAnswers { get; set; }
        public DbSet<UserRatingSurveyQuestionAnswer> UserRatingSurveyQuestionsAnswers { get; set; }
        public DbSet<UserSurveyResult> UsersSurveyResults { get; set; }
        #endregion

        #region Test Entities
        public DbSet<TestTemplate> TestTemplates { get; set; }
        public DbSet<BoolTestQuestionTemplate> BoolTestQuestionsTemplates { get; set; }
        public DbSet<OptionsTestQuestionTemplate> OptionsTestQuestionsTemplates { get; set; }
        public DbSet<TestOptionQuestionAnswerTemplate> TestOptionQuestionsAnswerTemplates { get; set; }
        public DbSet<UserBoolTestQuestionAnswer> UserBoolTestQuestionsAnswers { get; set; }
        public DbSet<UserOptionsTestQuestionAnswer> UserOptionsTestQuestionsAnswers { get; set; }
        public DbSet<UserTestResult> UsersTestResults { get; set; }
        #endregion

        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
