using System.Collections.Generic;

namespace App.Model.Entities.Common
{
    public abstract class BaseChat : AuditableEntity
    {
        public bool IsHidden { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
