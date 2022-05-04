using App.Model.Enums;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayersTrainingScoresRankingDto
    {
        public TrainingScoreType ScoreType { get; set; }
        public IEnumerable<PlayerRankingScoreValueDto> PlayerScores { get; set; }
    }
}
