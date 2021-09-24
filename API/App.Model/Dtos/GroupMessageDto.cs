using App.Model.Dtos.Common;
using System;

namespace App.Model.Dtos
{
    public class GroupMessageDto : AuditableEntityDto
    {
        public string Content { get; set; }

        public UserDto Sender { get; set; }
        public bool IsHidden { get; set; }
        public DateTime Sended { get; set; }
        public GroupChatDto Chat { get; set; }
    }
}
