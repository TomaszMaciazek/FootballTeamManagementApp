using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerMonthlyMatchScoreStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<MonthlyMatchScoreStatisticsDto> ScoreStatistics { get; set; }
    }
}
