using System;

namespace App.Model.Dtos.History
{
    public class TeamHistoryMatchEventDto
    {
        public Guid Id { get; set; }
        public SimpleMatchDto Match { get; set; }
    }
}
