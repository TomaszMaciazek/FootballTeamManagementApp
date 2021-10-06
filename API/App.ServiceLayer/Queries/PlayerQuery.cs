using App.Model.Enums;

namespace App.ServiceLayer.Queries
{
    public class PlayerQuery : UserQuery
    {
        public int? CountryId { get; set; }
        public Gender? Gender { get; set; }
    }
}
