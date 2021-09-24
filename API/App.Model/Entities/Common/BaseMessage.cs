using App.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.Common
{
    public class BaseMessage : AuditableEntity
    {

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public User Sender { get; set; }

        [Required]
        public bool IsHidden { get; set; }

        [Required]
        public DateTime Sended { get; set; }
    }
}
