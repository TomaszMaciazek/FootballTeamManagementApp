using App.Model.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Message : AuditableEntity
    {
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Content { get; set; }
        public User Sender { get; set; }
        public DateTime? SendDate { get; set; }
        public ICollection<MessageTransmission> Transmissions { get; set; }
        public ICollection<MessageRecipient> Recipients { get; set; }
    }
}
