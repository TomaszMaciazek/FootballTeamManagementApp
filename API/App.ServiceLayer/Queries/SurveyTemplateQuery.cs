namespace App.ServiceLayer.Queries
{
    public class SurveyTemplateQuery : PaginationQuery
    {
        public string Title { get; set; }
        public bool? IsAnonymous { get; set; }
    }
}
