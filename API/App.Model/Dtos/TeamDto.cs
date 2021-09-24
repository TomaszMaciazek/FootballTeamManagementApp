using App.Model.Dtos.Common;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class TeamDto : AuditableEntityDto
    {
        public string Name { get; set; }
        public ICollection<PlayerDto> Players { get; set; }
        public CoachDto MainCoach { get; set; }
    }
}
