using System;

namespace App.Model.ViewModels.Queries
{
    public class TeamQuery : PaginationQuery
    {
        public string Name { get; set; }
        public Guid? CoachId { get; set; }
    }
}
