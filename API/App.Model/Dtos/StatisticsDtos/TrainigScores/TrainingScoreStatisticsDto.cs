using App.Model.Enums;

namespace App.Model.Dtos.StatisticsDtos
{
    public class TrainingScoreStatisticsDto
    {
        public TrainingScoreType ScoreType { get; set; }
        public double Avg{ get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Median { get; set; }
    }
}
