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

        [Required(AllowEmptyStrings = false)]
        public string OpponentsClubName { get; set; }
        public int? OpponentsScore { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public PlayersGender PlayersGender { get; set; }

        [Required]
        public bool IsModificationForbidden { get; set; }

        public ICollection<MatchPlayer> Players { get; set; }
        public ICollection<MatchPoint> Points { get; set; }
        public ICollection<PlayerCard> PlayersCards { get; set; }
        public ICollection<CoachCard> CoachesCards { get; set; }
        public ICollection<Coach> Coaches { get; set; }
    }
}
