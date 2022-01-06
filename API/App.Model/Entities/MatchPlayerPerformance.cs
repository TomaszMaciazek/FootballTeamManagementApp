using App.Model.Entities.Common;
using App.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class MatchPlayerPerformance : AuditableEntity
    {
        public Match Match { get; set; }
        public Player Player { get; set; }
        public PlayerHistory PlayerHistory { get; set; }
        public Team Team { get; set; }

        [Required]
        public PlayerPosition PlayerPosition { get; set; }
    }
}
