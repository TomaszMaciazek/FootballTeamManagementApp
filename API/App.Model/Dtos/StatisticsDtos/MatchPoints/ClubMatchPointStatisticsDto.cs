using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class ClubMatchPointStatisticsDto
    {
        public IEnumerable<MatchPointCountDto> CupMatchesPoints { get; set; }
        public IEnumerable<MatchPointCountDto> LeagueMatchesPoints { get; set; }
        public IEnumerable<MatchPointCountDto> FriendlyMatchPoints { get; set; }
    }
}
