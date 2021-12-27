using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Queries
{
    public class CoachQuery : UserQuery
    {
        public IEnumerable<int> CountriesIds { get; set; }
        public Gender? Gender { get; set; }
        public IEnumerable<Guid> TeamsIds { get; set; }
        public bool? IsStillWorking{ get; set; }
    }
}
