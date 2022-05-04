using App.Model.Entities.Common;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class PlayerHistory : AuditableEntity 
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public ICollection<PlayerJoinedTeamEvent> PlayerJoinedTeamEvents { get; set; }
        public ICollection<MatchPlayerPerformance> MatchPlayerPerformances { get; set; }
        public ICollection<PlayerLeftTeamEvent> PlayerLeftTeamEvents { get; set; }
    }
}
