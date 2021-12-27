namespace App.Model.ViewModels.Queries
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderByColumnName { get; set; }
        public string OrderByDirection { get; set; }
    }
}
