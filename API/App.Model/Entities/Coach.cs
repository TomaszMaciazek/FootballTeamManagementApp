using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Coach : AuditableEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public Gender Gender { get; set; }
        public DateTime? StartedWorking { get; set; }
        public DateTime? FinishedWorking { get; set; }
        public ICollection<Match> Matches { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<CoachCard> Cards { get; set; }
    }
}
