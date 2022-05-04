using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class MonthlyTrainingScoreStatisticsDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<TrainingScoreStatisticsDto> Scores { get; set; }
    }
}
