using App.Model.Entities.Common;
using App.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Model.Entities
{
    public class TrainingScore : AuditableEntity
    {
        public User Modifier { get; set; }
        public Player Player { get; set; }
        public Training Training { get; set; }

        [Required]
        public TrainingScoreType ScoreType { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "The score must be in range <1,10>")]
        public double Value { get; set; }
    }
}
