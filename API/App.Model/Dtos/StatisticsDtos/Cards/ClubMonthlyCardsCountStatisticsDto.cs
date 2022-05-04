using System.Collections.Generic;

namespace App.Model.Dtos.StatisticsDtos
{
    public class ClubMonthlyCardsCountStatisticsDto
    {
        public IEnumerable<MonthDataCountStatisticsDto> PlayersYellowCards { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> PlayersRedCards { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> CoachesYellowCards { get; set; }
        public IEnumerable<MonthDataCountStatisticsDto> CoachesRedCards { get; set; }
    }
}
