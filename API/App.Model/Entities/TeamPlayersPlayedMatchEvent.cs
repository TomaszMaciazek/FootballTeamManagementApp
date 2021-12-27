using System;

namespace App.Model.Entities
{
    public class TeamPlayersPlayedMatchEvent
    {
        public Guid Id { get; set; }
        public TeamHistory TeamHistory { get; set; }
        public Match Match { get; set; }
    }
}
