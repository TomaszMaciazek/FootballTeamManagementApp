using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Dtos.History
{
    public class TeamHistoryPlayerJoinedTeamEventDto
    {
        public Guid Id { get; set; }
        public SimplePlayerNameDto Player { get; set; }
        public DateTime Date { get; set; }
    }
}
