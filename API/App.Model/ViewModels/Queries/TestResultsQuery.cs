using System;

namespace App.Model.ViewModels.Queries
{
    public class TestResultsQuery : PaginationQuery
    {
        public Guid TestId { get; set; }
    }
}
