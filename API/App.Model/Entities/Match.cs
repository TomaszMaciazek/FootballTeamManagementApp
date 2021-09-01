using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Match : AuditableEntity
    {
        [Required]
        public DateTime Date { get; set; }
        public int? OpponentsScore { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public PlayersGender PlayersGender { get; set; }

        [Required]
        public bool IsDeleteForbidden { get; set; }

        public ICollection<MatchPlayer> Players { get; set; }
        public ICollection<MatchPoint> Points { get; set; }
        public ICollection<Card> Cards { get; set; }
        public Coach Coach { get; set; }
    }
}
