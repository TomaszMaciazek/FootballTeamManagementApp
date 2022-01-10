using App.Model.Enums;
using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class MatchScoreListItemDto
    {
        public Guid Id { get; set; }
        public SimplePlayerNameDto Player { get; set; }
        public MatchScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
