using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Queries
{
    public class MessageQuery : PaginationQuery
    {
        public Guid UserId { get; set; }
        public MailboxType MailboxType { get; set; }
        public string SearchText { get; set; }
    }
}
