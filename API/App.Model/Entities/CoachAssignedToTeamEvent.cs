using System;

namespace App.Model.Entities
{
    public class CoachAssignedToTeamEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TeamHistory TeamHistory { get; set; }
        public Coach Coach { get; set; }
    }
}
