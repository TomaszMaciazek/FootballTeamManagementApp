using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class MonthlyMatchScoreStatisticsDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<MatchScoreStatisticsDto> Scores { get; set; }
    }
}
