using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class CoachCardDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public SimpleCoachDto Coach { get; set; }
        public Guid MatchId { get; set; }
        public CardColor Color { get; set; }
        public int Count { get; set; }
    }
}
