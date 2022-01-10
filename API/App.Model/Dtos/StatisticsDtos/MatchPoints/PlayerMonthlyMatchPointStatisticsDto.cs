using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerMonthlyMatchPointStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<MonthlyMatchPointCountDto> CupMatchesPoints { get; set; }
        public IEnumerable<MonthlyMatchPointCountDto> LeagueMatchesPoints { get; set; }
        public IEnumerable<MonthlyMatchPointCountDto> FriendlyMatchesPoints { get; set; }
    }
}
