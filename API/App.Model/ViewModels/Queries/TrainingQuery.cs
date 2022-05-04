using System;

namespace App.Model.ViewModels.Queries
{
    public class TrainingQuery : PaginationQuery
    {
        public bool? IsActive { get; set; }
        public string Title { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
