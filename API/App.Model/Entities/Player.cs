using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Player : AuditableEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public DateTime? FinishedPlaying { get; set; }
        public Team Team { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public PlayerPosition PrefferedPosition { get; set; }

        public ICollection<MatchPlayer> Matches { get; set; }
        public ICollection<MatchPoint> MatchPoints { get; set; }
        public ICollection<PlayerCard> Cards { get; set; }
        public ICollection<TrainingScore> TrainingScores { get; set; }

    }
}
