using App.Model.Enums;
using System;

namespace App.Model.Entities
{
    public class BaseMessage : AuditableEntity
    {
        public string Content { get; set; }
        public User Sender { get; set; }
        public bool IsHidden { get; set; }
        public MessageType Type { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime Sended { get; set; }
    }
}
