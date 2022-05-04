using System;

namespace App.Model.Dtos.StatisticsDtos
{
    public class PlayerCardStatisticsDto
    {
        public Guid PlayerId { get; set; }
        public int YellowCardsCount { get; set; }
        public int RedCardsCount { get; set; }
        public double CardsAvg { get; set; }

    }
}
