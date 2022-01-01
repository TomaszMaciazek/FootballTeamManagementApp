using System;

namespace App.Model.Dtos.History
{
    public class TeamHistoryPlayerLeftTeamEventDto
    {
        public Guid Id { get; set; }
        public SimplePlayerNameDto Player { get; set; }
        public DateTime Date { get; set; }
    }
}
