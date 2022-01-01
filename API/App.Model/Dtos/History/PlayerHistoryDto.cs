using System;
using System.Collections.Generic;

namespace App.Model.Dtos.History
{
    public class PlayerHistoryDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<PlayerHistoryPlayerJoinedTeamEventDto> PlayerJoinedTeamEvents { get; set; }
        public IEnumerable<PlayerHistoryPlayerLeftTeamEventDto> PlayerLeftTeamEvents { get; set; }
        public IEnumerable<PlayerHistoryMatchDto> MatchEvents { get; set; }
    }
}
