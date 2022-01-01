using App.Model.Enums;
using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class MonthlyMatchPointCountDto
    {
        public IEnumerable<MatchPointCountDto> PointsByType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
