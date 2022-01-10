using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public SimpleUserDto Sender { get; set; }
        public IEnumerable<SimpleUserDto> Recipients { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public MailboxType MailboxType { get; set; }
        public bool IsRead { get; set; }
    }
}
