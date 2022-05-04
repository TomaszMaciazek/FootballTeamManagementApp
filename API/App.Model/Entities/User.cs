using App.Model.Entities.SurveyEntities;
using App.Model.Entities.TestEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Username { get; set; }

        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }

        [Required]
        public int BadLogonCount { get; set; } = 0;
        public DateTime? AccountLockoutTime { get; set; }
        public DateTime? LastLogon { get; set; }

        [Required]
        public DateTime LastPasswordSet { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<SurveyTemplate> UserSurveysTemplates { get; set; }
        public ICollection<TestTemplate> UserTestsTemplates { get; set; }
        public ICollection<UserSurveyResult> SurveysResults { get; set; }
        public ICollection<UserTestResult> TestsResults { get; set; }
        public ICollection<TrainingScore> CreatedTrainingScores { get; set; }
        public ICollection<MessageTransmission> Messages { get; set; }
        public ICollection<MessageRecipient> MessagesReceived { get; set; }
        public Role Role { get; set; }
        public Player PlayerDetails { get; set; }
        public Coach CoachDetails { get; set; }
    }
}
