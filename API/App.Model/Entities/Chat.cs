using System.Collections.Generic;

namespace App.Model.Entities
{
    public abstract class Chat : AuditableEntity
    {
        public bool IsHidden { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
