using System;

namespace App.Model.Entities
{
    public class PlayerLeftTeamEvent
    {
        public Guid Id { get; set; }
        public PlayerHistory PlayerHistory { get; set; }
        public TeamHistory TeamHistory { get; set; }
        public DateTime Date { get; set; }
    }
}
