using App.Model.Enums;
using System.Collections.Generic;

namespace App.Model.Dtos.Statistics
{
    public class PlayerTrainingScoreStatisticsDto
    {
        public SimpleTrainingDto Training { get; set; }
        public SimplePlayerNameDto Player { get; set; }
        public TrainingScoreType ScoreType { get; set; }
        public IEnumerable<double> Values { get; set; }
    }
}
