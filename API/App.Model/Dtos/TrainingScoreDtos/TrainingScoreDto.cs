using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class TrainingScoreDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public TrainingScoreType ScoreType { get; set; }
        public double Value { get; set; }
        public bool IsActive { get; set; }
    }
}
