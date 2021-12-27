using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class SimpleTrainingScoreDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public TrainingScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
