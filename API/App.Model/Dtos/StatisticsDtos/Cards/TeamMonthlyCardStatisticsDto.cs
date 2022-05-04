using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class TeamMonthlyCardStatisticsDto
    {
        public Guid TeamId { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> YellowCards { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> RedCards { get; set; }
    }
}
