using System;

namespace App.Model.Entities.Common
{
    public abstract class EditableEntity
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
