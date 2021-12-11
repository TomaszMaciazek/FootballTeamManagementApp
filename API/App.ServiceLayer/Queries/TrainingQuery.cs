using System;

namespace App.ServiceLayer.Queries
{
    public class TrainingQuery : PaginationQuery
    {
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
