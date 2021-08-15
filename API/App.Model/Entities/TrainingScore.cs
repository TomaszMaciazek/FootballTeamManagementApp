﻿using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class TrainingScore : AuditableEntity
    {
        public Player Player { get; set; }
        public Training Training { get; set; }

        [Range(1, 10, ErrorMessage = "The score must be in range <1,10>")]
        public int Score { get; set; }
    }
}
