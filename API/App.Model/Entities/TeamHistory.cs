using App.Model.Entities.Common;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class TeamHistory : AuditableEntity
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<PlayerJoinedTeamEvent> PlayerJoinedTeamEvents { get; set; }
        public ICollection<CoachAssignedToTeamEvent> CoachAssignedToTeamEvents { get; set; }
        public ICollection<TeamPlayersPlayedMatchEvent> TeamPlayersPlayedMatchEvents { get; set; }
        public ICollection<PlayerLeftTeamEvent> PlayerLeftTeamEvents { get; set; }
    }
}
