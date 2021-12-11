using App.Model.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Training : AuditableEntity
    {
        [Required]
        public DateTime Date { get; set; }
        public ICollection<TrainingScore> Scores { get; set; }
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
