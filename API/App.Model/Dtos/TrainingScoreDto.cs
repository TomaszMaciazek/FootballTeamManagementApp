using App.Model.Enums;

namespace App.Model.Dtos
{
    public class TrainingScoreDto
    {
        public PlayerDto Player { get; set; }
        public TrainingDto Training { get; set; }
        public TrainingScoreType ScoreType { get; set; }
        public double Value { get; set; }
    }
}
