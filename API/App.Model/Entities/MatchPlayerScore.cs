using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class MatchPlayerScore : AuditableEntity
    {
        public Player Player { get; set; }
        public Match Match { get; set; }
        [Required]
        public MatchScoreType ScoreType { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "The score must be in range <1,10>")]
        public double Value { get; set; }
    }
}
