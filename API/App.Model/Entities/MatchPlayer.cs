namespace App.Model.Entities
{
    public class MatchPlayer : AuditableEntity
    {
        public Match Match { get; set; }
        public Player Player { get; set; }
        public string PlayerPosition { get; set; }
    }
}
