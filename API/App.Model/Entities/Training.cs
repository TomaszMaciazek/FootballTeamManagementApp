using App.Model.Entities.Common;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class Training : AuditableEntity
    {
        public DateTime Date { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Coach> Coaches { get; set; }
        public ICollection<TrainingScore> Scores { get; set; }
        public string Description { get; set; }
    }
}
