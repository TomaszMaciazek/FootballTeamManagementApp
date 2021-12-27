using System;

namespace App.Model.ViewModels.Queries
{
    public class MessageQuery : PaginationQuery
    {
        public Guid? ChatId { get; set; }
        public string Phrase { get; set; }
        public Guid? SenderId { get; set; }
    }
}
