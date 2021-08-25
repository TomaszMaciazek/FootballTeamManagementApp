using App.Model.Entities.SurveyEntities;
using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Entities.TestEntities;
using App.Model.Entities.TestEntities.AnswersResults;
using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }

        [Required]
        public int BadLogonCount { get; set; }
        public UserImage Image { get; set; }
        public DateTime? AccountLockoutTime { get; set; }
        public DateTime? LastBadPasswordAttempt { get; set; }
        public DateTime? LastLogon { get; set; }
        public DateTime? LastPasswordSet { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public UserRole Role { get; set; }
        public ICollection<IndividualChat> IndividualChats { get; set; }
        public ICollection<GroupChat> GroupChats { get; set; }
        public ICollection<IndividualMessage> IndividualMessages { get; set; }
        public ICollection<GroupMessage> GroupMessages { get; set; }
        public ICollection<SurveyTemplate> UserSurveysTemplates { get; set; }
        public ICollection<TestTemplate> UserTestsTemplates { get; set; }
        public ICollection<UserSurveyResult> SurveysResults { get; set; }
        public ICollection<UserTestResult> TestsResults { get; set; }
        public ICollection<UserBoolSurveyQuestionAnswer> BooleanSurveyQuestionAnswers { get; set; }
        public ICollection<UserTextSurveyQuestionAnswer> TextSurveyQuestionAnswers { get; set; }
        public ICollection<UserOptionsSurveyQuestionAnswer> OptionsSurveyQuestionAnswers { get; set; }
        public ICollection<UserRatingSurveyQuestionAnswer> RatingSurveyQuestionAnswers { get; set; }
        public ICollection<UserBoolTestQuestionAnswer> BoolTestQuestionAnswers { get; set; }
        public ICollection<UserOptionsTestQuestionAnswer> OptionsTestQuestionAnswers { get; set; }
        public Player PlayerDetails { get; set; }
        public Coach CoachDetails { get; set; }
    }
}
