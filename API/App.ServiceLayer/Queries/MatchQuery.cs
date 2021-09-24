using App.Model.Enums;
using System;

namespace App.ServiceLayer.Queries
{
    public class MatchQuery : PaginationQuery
    {
        public string Location { get; set; }
        public string OpponentsClubName { get; set; }
        public PlayersGender? PlayersGender { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
