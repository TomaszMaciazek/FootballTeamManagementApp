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
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace App.DataAccess.Interfaces
{
    public interface IApplicationDbContext
    {
        #region Match Entities
         DbSet<PlayerCard> PlayersCards { get; set; }
         DbSet<CoachCard> CoachesCards { get; set; }
         DbSet<Match> Matches { get; set; }
         DbSet<MatchPoint> MatchPoints { get; set; }
         DbSet<MatchPlayer> MatchPlayers { get; set; }
        #endregion

        #region Chat Entities
         DbSet<GroupChat> GroupChats { get; set; }
         DbSet<GroupChatImage> GroupChatImages { get; set; }
         DbSet<IndividualChat> IndividualChats { get; set; }
         DbSet<GroupMessage> GroupMessages { get; set; }
         DbSet<IndividualMessage> IndividualMessages { get; set; }
        #endregion

        #region Identity Entites
         DbSet<User> Users { get; set; }
         DbSet<Coach> Coaches { get; set; }
         DbSet<Player> Players { get; set; }
         DbSet<UserImage> UserImages { get; set; }
         DbSet<Role> Roles { get; set; }
         DbSet<RoleClaim> RoleClaims { get; set; }
        #endregion

        #region Training Entities
         DbSet<Training> Trainings { get; set; }
         DbSet<TrainingScore> TrainingScores { get; set; }
         DbSet<Team> Teams { get; set; }
        #endregion

        #region Survey Entites
         DbSet<SurveyTemplate> SurveyTemplates { get; set; }
         DbSet<TextSurveyQuestionTemplate> TextSurveyQuestionsTemplates { get; set; }
         DbSet<BoolSurveyQuestionTemplate> BoolSurveyQuestionsTemplates { get; set; }
         DbSet<OptionsSurveyQuestionTemplate> OptionsSurveyQuestionsTemplates { get; set; }
         DbSet<RatingSurveyQuestionTemplate> RatingSurveyQuestionsTemplates { get; set; }
         DbSet<SurveyOptionQuestionAnswerTemplate> SurveyOptionQuestionsAnswerTemplates { get; set; }
         DbSet<UserBoolSurveyQuestionAnswer> UserBoolSurveyQuestionsAnswers { get; set; }
         DbSet<UserOptionsSurveyQuestionAnswer> UserOptionsSurveyQuestionsAnswers { get; set; }
         DbSet<UserTextSurveyQuestionAnswer> UserTextSurveyQuestionsAnswers { get; set; }
         DbSet<UserRatingSurveyQuestionAnswer> UserRatingSurveyQuestionsAnswers { get; set; }
         DbSet<UserSurveyResult> UsersSurveyResults { get; set; }
        #endregion

        #region Test Entities
         DbSet<TestTemplate> TestTemplates { get; set; }
         DbSet<BoolTestQuestionTemplate> BoolTestQuestionsTemplates { get; set; }
         DbSet<OptionsTestQuestionTemplate> OptionsTestQuestionsTemplates { get; set; }
         DbSet<TestOptionQuestionAnswerTemplate> TestOptionQuestionsAnswerTemplates { get; set; }
         DbSet<UserBoolTestQuestionAnswer> UserBoolTestQuestionsAnswers { get; set; }
         DbSet<UserOptionsTestQuestionAnswer> UserOptionsTestQuestionsAnswers { get; set; }
         DbSet<UserTestResult> UsersTestResults { get; set; }
        #endregion

        #region Site Entities
        DbSet<Language> Languages { get; set; }
        DbSet<Translation> Translations { get; set; }
        DbSet<Country> Countries { get; set; }
        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
    }
}
