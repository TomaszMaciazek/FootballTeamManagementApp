using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class TeamMatchPointStatisticsDto
    {
        public Guid TeamId { get; set; }
        public IEnumerable<MatchPointCountDto> CupMatchesPoints { get; set; }
        public IEnumerable<MatchPointCountDto> LeagueMatchesPoints { get; set; }
        public IEnumerable<MatchPointCountDto> FriendlyMatchPoints { get; set; }
    }
}
