using App.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class GroupChatImage : AuditableEntity
    {
        [Required]
        public Guid ChatId { get; set; }
        public GroupChat Chat { get; set; }
        public byte[] Data { get; set; }
    }
}
