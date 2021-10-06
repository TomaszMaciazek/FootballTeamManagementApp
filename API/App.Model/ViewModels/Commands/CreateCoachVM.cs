using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateCoachVM : CreateUserVM
    {
        public Guid UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? StartedWorking { get; set; }
        public IEnumerable<Guid> TeamsIds { get; set; }
    }
}
