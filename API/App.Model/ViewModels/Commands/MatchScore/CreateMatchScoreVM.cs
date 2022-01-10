using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateMatchScoreVM
    {
        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
        public MatchScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
