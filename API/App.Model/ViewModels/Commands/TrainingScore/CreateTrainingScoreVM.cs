using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateTrainingScoreVM
    {
        public Guid PlayerId { get; set; }
        public Guid TrainingId { get; set; }
        public TrainingScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
