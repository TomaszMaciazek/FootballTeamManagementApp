﻿using System;

namespace App.Model.Dtos.Common
{
    public class AuditableEntityDto
    {
        public Guid? Id { get; set; }
        public bool? IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
