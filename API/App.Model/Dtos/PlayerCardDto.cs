using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class PlayerCardDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
        public CardColor Color { get; set; }
        public int Count { get; set; }
    }
}
