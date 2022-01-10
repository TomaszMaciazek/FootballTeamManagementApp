using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerMonthlyTrainingScoreStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<MonthlyTrainingScoreStatisticsDto> ScoreStatistics { get; set; }
    }
}
