using App.Model.Dtos.StatisticsDtos;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class ClubMonthlyTrainingScoreStatisticsDto
    {
        public IEnumerable<MonthlyTrainingScoreStatisticsDto> ScoreStatistics { get; set; }
    }
}
