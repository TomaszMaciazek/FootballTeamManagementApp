using App.Model.Enums;

namespace App.Model.Dtos.StatisticsDtos
{
    public class MatchPointCountDto
    {
        public MatchPointType Type { get; set; }
        public int Count { get; set; }
    }
}
