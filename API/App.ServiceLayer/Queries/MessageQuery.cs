using System;

namespace App.ServiceLayer.Queries
{
    public class MessageQuery : PaginationQuery
    {
        public Guid? ChatId { get; set; }
        public string Phrase { get; set; }
        public Guid? SenderId { get; set; }
    }
}
