using App.Model.Entities.Common;
using App.Model.Enums;

namespace App.Model.Entities
{
    public class Card : AuditableEntity
    {
        public Player Player { get; set; }
        public Match Match { get; set; }
        public CardColor Color { get; set; }
    }
}
