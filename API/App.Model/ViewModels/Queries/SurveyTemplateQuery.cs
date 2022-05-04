using System;

namespace App.Model.ViewModels.Queries
{
    public class SurveyTemplateQuery : PaginationQuery
    {
        public Guid? AuthorId { get; set; }
        public string Title { get; set; }
        public bool? IsAnonymous { get; set; }
    }
}
