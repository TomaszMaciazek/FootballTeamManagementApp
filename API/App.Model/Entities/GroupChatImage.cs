using App.Model.Entities.Common;
using System;

namespace App.Model.Entities
{
    public class GroupChatImage : AuditableEntity
    {
        public Guid ChatId { get; set; }
        public GroupChat Chat { get; set; }
        public byte[] Data { get; set; }
    }
}
