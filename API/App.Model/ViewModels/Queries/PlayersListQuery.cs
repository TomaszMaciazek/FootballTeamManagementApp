using App.Model.Enums;

namespace App.Model.ViewModels.Queries
{
    public class PlayersListQuery : PaginationQuery
    {
        public string TeamName { get; set; }
        public string NameQuery { get; set; }
        public Gender? Gender { get; set; }
        public PlayerPosition? PrefferedPosition { get; set; }
        public string CountryCode { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
