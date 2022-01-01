using System;

namespace App.Model.Dtos.History
{
    public class PlayerHistoryPlayerLeftTeamEventDto
    {
        public Guid Id { get; set; }
        public SimpleTeamDto Team { get; set; }
        public DateTime Date { get; set; }
    }
}
