using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class CoachAccountDto
    {
        public Guid Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public Gender Gender { get; set; }
        public DateTime? StartedWorking { get; set; }
        public DateTime? FinishedWorking { get; set; }
        public IEnumerable<SimpleTeamDto> Teams { get; set; }
    }
}
