using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerMonthlyCardStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> YellowCards { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> RedCards { get; set; }
    }
}
