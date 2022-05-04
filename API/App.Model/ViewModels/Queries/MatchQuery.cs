using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Queries
{
    public class MatchQuery : PaginationQuery
    {
        public string QueryString { get; set; }
        public IEnumerable<PlayersGender> PlayersGenders { get; set; }
        public IEnumerable<MatchType> MatchTypes { get; set; }
        public IEnumerable<Guid> TeamsIds { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
