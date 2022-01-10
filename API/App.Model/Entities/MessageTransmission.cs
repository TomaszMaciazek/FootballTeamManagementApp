using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Entities
{
    public class MessageTransmission : AuditableEntity
    {
        public User MailboxOwner { get; set; }
        public Message Message { get; set; }
        [Required]
        public MailboxType MailboxType { get; set; }
        public bool IsRead { get; set; }
    }
}
