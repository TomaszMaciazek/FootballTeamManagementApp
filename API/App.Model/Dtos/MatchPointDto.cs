using App.Model.Dtos.Common;
using App.Model.Enums;

namespace App.Model.Dtos
{
    public class MatchPointDto : AuditableEntityDto
    {
        public MatchDto Match { get; set; }
        public PlayerDto GoalScorer { get; set; }
        public MatchPointType Point { get; set; }
        public int MinuteOfMatch { get; set; }
    }
}
