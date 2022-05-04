using System;

namespace App.Model.Dtos.StatisticsDtos
{
    public class TeamCardStatisticsDto
    {
        public Guid TeamId { get; set; }
        public int YellowCardsCount { get; set; }
        public int RedCardsCount { get; set; }
    }
}
