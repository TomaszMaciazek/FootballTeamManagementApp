using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class SimpleMatchScoreDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid PlayerId { get; set; }
        public MatchScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
