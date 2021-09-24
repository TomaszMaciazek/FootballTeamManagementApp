using System;

namespace App.ServiceLayer.Queries
{
    public class TrainingQuery : PaginationQuery
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
