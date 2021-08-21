using App.Model.Entities.Common;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class Player : AuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public DateTime? FinishedPlaying { get; set; }
        public Team Team { get; set; }
        public ICollection<MatchPlayer> Matches { get; set; }
        public ICollection<MatchPoint> MatchPoints { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<TrainingScore> TrainingScores { get; set; }

    }
}
