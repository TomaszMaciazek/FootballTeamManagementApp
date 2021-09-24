namespace App.Model.Dtos
{
    public class TrainingScoreDto
    {
        public PlayerDto Player { get; set; }
        public TrainingDto Training { get; set; }
        public int Score { get; set; }
    }
}
