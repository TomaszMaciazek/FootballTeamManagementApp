using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerTrainingScoreStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<TrainingScoreStatisticsDto> Scores { get; set; }
    }
}
