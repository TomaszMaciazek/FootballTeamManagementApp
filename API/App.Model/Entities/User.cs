using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public int BadLogonCount { get; set; }
        public UserImage Image { get; set; }
        public DateTime? AccountLockoutTime { get; set; }
        public DateTime? LastBadPasswordAttempt { get; set; }
        public DateTime? LastLogon { get; set; }
        public DateTime? LastPasswordSet { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }
        public ICollection<SurveyTemplate> UserSurveysTemplates { get; set; }
        public ICollection<UserSurveyResult> SurveysResults { get; set; }
        public ICollection<UserBooleanQuestionAnswer> BooleanQuestionAnswers { get; set; }
        public ICollection<UserTextQuestionAnswer> TextQuestionAnswers { get; set; }
        public ICollection<UserOptionsQuestionAnswer> OptionsQuestionAnswers { get; set; }
        public ICollection<UserRatingQuestionAnswer> RatingQuestionAnswers { get; set; }
        public Player PlayerDetails { get; set; }
        public Coach CoachDetails { get; set; }
    }
}
