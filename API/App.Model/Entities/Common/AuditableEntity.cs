using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.Common
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
