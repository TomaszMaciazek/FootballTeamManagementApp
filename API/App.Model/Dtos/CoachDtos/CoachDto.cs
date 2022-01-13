using App.Model.Dtos.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class CoachDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public Gender Gender { get; set; }
        public DateTime? StartedWorking { get; set; }
        public DateTime? FinishedWorking { get; set; }
        public IEnumerable<SimpleTeamDto> Teams { get; set; }
    }
}
