using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
