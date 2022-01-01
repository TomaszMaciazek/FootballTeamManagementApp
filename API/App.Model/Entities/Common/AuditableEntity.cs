using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.Common
{
    public abstract class AuditableEntity : EditableEntity
    {
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
