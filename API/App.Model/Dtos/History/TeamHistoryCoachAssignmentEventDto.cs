using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Dtos.History
{
    public class TeamHistoryCoachAssignmentEventDto
    {
        public Guid Id { get; set; }
        public SimpleCoachDto Coach { get; set; }
        public DateTime Date { get; set; }
    }
}
