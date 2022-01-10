using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerMatchScoreStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<MatchScoreStatisticsDto> Scores { get; set; }
    }
}
