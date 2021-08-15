using App.Model.Enums;

namespace App.Model.Entities
{
    public class MatchPoint : AuditableEntity
    {
        public Match Match { get; set; }
        public Player GoalScorer { get; set; }
        public MatchPointType Point { get; set; }
        public int MinuteOfMatch { get; set; }
    }
}
