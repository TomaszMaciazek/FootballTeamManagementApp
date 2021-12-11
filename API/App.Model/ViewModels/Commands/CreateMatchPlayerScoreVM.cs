using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateMatchPlayerScoreVM
    {
        public Guid PlayerPerformanceId { get; set; }
        public MatchScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
