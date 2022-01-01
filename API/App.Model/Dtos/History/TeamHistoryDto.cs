using System;
using System.Collections.Generic;

namespace App.Model.Dtos.History
{
    public class TeamHistoryDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<TeamHistoryMatchEventDto> MatchEvents { get; set; }
        public IEnumerable<TeamHistoryPlayerJoinedTeamEventDto> PlayerJoinedTeamEvents { get; set; }
        public IEnumerable<TeamHistoryPlayerLeftTeamEventDto> PlayerLeftTeamEvents { get; set; }
        public IEnumerable<TeamHistoryCoachAssignmentEventDto> CoachAssignmentEvents { get; set; }
    }
}
