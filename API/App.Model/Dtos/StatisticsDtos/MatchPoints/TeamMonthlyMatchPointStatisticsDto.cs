using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class TeamMonthlyMatchPointStatisticsDto
    {
        public Guid TeamId { get; set; }
        public IEnumerable<MonthlyMatchPointCountDto> CupMatchesPoints { get; set; }
        public IEnumerable<MonthlyMatchPointCountDto> LeagueMatchesPoints { get; set; }
        public IEnumerable<MonthlyMatchPointCountDto> FriendlyMatchesPoints { get; set; }
    }
}
