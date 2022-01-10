using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class ClubMonthlyMatchScoreStatisticsDto
    {
        public IEnumerable<MonthlyMatchScoreStatisticsDto> ScoreStatistics { get; set; }
    }
}
