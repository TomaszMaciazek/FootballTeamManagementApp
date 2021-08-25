using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.Common
{
    public abstract class BaseChat : AuditableEntity
    {
        [Required]
        public bool IsHidden { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
