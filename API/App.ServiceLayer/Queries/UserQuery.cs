namespace App.ServiceLayer.Queries
{
    public class UserQuery : PaginationQuery
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
