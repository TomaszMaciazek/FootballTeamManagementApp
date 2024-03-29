﻿using App.Model.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Team : AuditableEntity
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(256)]
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
        public Coach MainCoach { get; set; }
        public TeamHistory History { get; set; }
        public ICollection<MatchPoint> MatchPoints { get; set; }
        public ICollection<PlayerCard> PlayersCards { get; set; }
        public ICollection<MatchPlayerPerformance> PlayerPerformances { get; set; }
    }
}
