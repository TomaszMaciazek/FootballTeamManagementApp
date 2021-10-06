using App.Model.Enums;

namespace App.ServiceLayer.Queries
{
    public class CoachQuery : UserQuery
    {
        public int? CountryId { get; set; }
        public Gender? Gender { get; set; }
    }
}
