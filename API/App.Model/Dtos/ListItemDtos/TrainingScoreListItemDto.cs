using App.Model.Enums;
using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class TrainingScoreListItemDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime LastModyfication { get; set; }
        public string LastModifier { get; set; }
        public TrainingScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
