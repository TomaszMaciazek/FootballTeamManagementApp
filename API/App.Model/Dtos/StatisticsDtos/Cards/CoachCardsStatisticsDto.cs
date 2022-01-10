using System;

namespace App.Model.Dtos.StatisticsDtos
{
    public class CoachCardsStatisticsDto
    {
        public Guid CoachId { get; set; }
        public int YellowCardsCount { get; set; }
        public int RedCardsCount { get; set; }
    }
}
