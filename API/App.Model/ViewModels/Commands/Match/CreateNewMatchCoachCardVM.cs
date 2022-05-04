using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateNewMatchCoachCardVM
    {
        public Guid CoachId { get; set; }
        public CardColor Color { get; set; }
        public int Count { get; set; }
    }
}
