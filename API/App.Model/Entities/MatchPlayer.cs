using App.Model.Entities.Common;
using App.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class MatchPlayer : AuditableEntity
    {
        public Match Match { get; set; }
        public Player Player { get; set; }

        [Required]
        public PlayerPosition PlayerPosition { get; set; }
    }
}
