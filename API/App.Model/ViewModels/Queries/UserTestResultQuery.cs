using System;

namespace App.Model.ViewModels.Queries
{
    public class UserTestResultQuery : PaginationQuery
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public bool? IsCompleated { get; set; }
    }
}
