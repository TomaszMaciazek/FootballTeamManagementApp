using System;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class CoachMonthlyCardStatisticsDto
    {
        public Guid CoachId { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> YellowCards { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> RedCards { get; set; }
    }
}
