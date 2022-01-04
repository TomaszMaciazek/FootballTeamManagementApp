using System;

namespace App.Model.ViewModels.Queries
{
    public class UserSurveyResultQuery : PaginationQuery
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public bool? IsCompleated { get; set; }
    }
}
