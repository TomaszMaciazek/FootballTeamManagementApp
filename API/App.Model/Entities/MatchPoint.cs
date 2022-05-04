using App.Model.Entities.Common;
using App.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class MatchPoint : AuditableEntity
    {
        public Match Match { get; set; }
        public Player GoalScorer { get; set; }
        public Team Team { get; set; }

        [Required]
        public MatchPointType Point { get; set; }

        [Required]
        public int MinuteOfMatch { get; set; }
    }
}
