using App.Model.Entities.Common;
using App.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Card : AuditableEntity
    {
        public Player Player { get; set; }
        public Match Match { get; set; }

        [Required]
        public CardColor Color { get; set; }
    }
}
