using System;

namespace App.ServiceLayer.Queries
{
    public class TeamQuery : PaginationQuery
    {
        public string Name { get; set; }
        public Guid? CoachId { get; set; }
    }
}
