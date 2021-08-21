using App.Model.Entities.Common;
using System;

namespace App.Model.Entities
{
    public class UserImage : AuditableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public byte[] Data { get; set; }
    }
}
