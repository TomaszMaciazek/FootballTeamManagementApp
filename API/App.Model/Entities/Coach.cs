using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class Coach : AuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime? StartedWorking { get; set; }
        public DateTime? FinishedWorking { get; set; }
        public ICollection<Match> Matches { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
