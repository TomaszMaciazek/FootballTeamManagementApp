﻿using App.Model.Entities;
using App.Model.Entities.SurveyEntities;
using App.Model.Entities.TestEntities;
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
        DbSet<MatchPlayerPerformance> MatchPlayersPerformances { get; set; }
        DbSet<MatchPlayerScore> MatchPlayersScores { get; set; }
        #endregion

        #region Message Entities
        DbSet<Message> Messages { get; set; }
        DbSet<MessageTransmission> MessageTransmissions { get; set; }
        DbSet<MessageRecipient> MessageUsers { get; set; }
        #endregion

        #region Identity Entites
        DbSet<User> Users { get; set; }
        DbSet<Coach> Coaches { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<RoleClaim> RoleClaims { get; set; }
        DbSet<PlayerHistory> PlayerHistories { get; set; }
        #endregion

        #region Training Entities
        DbSet<Training> Trainings { get; set; }
        DbSet<TrainingScore> TrainingScores { get; set; }
        #endregion

        #region History Events Entities
        DbSet<PlayerJoinedTeamEvent> PlayerJoinedTeamEvents { get; set; }
        DbSet<CoachAssignedToTeamEvent> CoachAssignedToTeamEvents { get; set; }
        DbSet<TeamPlayersPlayedMatchEvent> TeamPlayersPlayedMatchEvents { get; set; }
        DbSet<PlayerLeftTeamEvent> PlayerLeftTeamEvents { get; set; }
        #endregion

        #region Team Entities
        DbSet<Team> Teams { get; set; }
        DbSet<TeamHistory> TeamHistories { get; set; }
        #endregion

        #region Survey Entites
        DbSet<SurveyTemplate> SurveyTemplates { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionOption> SurveyQuestionOptions { get; set; }
        public DbSet<SurveySelectQuestionAnswer> SurveySelectQuestionAnswers { get; set; }
        public DbSet<SurveyTextQuestionAnswer> SurveyTextQuestionAnswers { get; set; }
        DbSet<UserSurveyResult> UsersSurveyResults { get; set; }
        #endregion

        #region Test Entities
         DbSet<TestTemplate> TestTemplates { get; set; }
         DbSet<UserTestResult> UsersTestResults { get; set; }
        #endregion

        #region Site Entities
        DbSet<Country> Countries { get; set; }
        #endregion

        DbSet<News> News { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
    }
}
