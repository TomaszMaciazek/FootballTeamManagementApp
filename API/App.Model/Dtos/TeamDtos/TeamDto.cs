using App.Model.Dtos.Common;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public IEnumerable<SimplePlayerDto> Players { get; set; }
        public CoachDto MainCoach { get; set; }
    }
}
