using App.Model.Dtos.Common;
using System;

namespace App.Model.Dtos
{
    public class IndividualMessageDto : AuditableEntityDto
    {
        public IndividualChatDto Chat { get; set; }
        public string Content { get; set; }
        public UserDto Sender { get; set; }
        public bool IsHidden { get; set; }
        public DateTime Sended { get; set; }
    }
}
