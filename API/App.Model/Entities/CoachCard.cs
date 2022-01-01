using App.Model.Entities;
using App.Model.Entities.Common;
using App.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class CoachCard : AuditableEntity
    {
        public Coach Coach { get; set; }
        public Match Match { get; set; }

        [Required]
        public CardColor Color { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
