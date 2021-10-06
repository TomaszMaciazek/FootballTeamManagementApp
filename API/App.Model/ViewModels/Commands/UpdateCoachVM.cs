using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class UpdateCoachVM
    {
        public Guid Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int? CountryId { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? StartedWorking { get; set; }
        public DateTime? FinishedWorking { get; set; }
        public IEnumerable<Guid> TeamsIds { get; set; }
    }
}
