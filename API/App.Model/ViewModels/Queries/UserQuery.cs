namespace App.Model.ViewModels.Queries
{
    public class UserQuery : PaginationQuery
    {
        public string QueryString { get; set; }
        public bool? IsActive { get; set; }
    }
}
