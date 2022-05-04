using System;

namespace App.Model.ViewModels.Queries
{
    public class TeamHistoryQuery
    {
        public Guid TeamId { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
