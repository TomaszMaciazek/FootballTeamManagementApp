using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateMatchPlayerPerformance
    {
        public Guid MatchId { get; set; }
        public Guid PlayerId { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
    }
}
