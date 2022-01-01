using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Dtos
{
    public class PlayerAccountDto
    {
        public Guid Id { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public DateTime? FinishedPlaying { get; set; }
        public Gender Gender { get; set; }
        public PlayerPosition PrefferedPosition { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public SimpleTeamDto Team { get; set; }
        public int MatchCount { get; set; }
    }
}
