using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class Match : AuditableEntity
    {
        public DateTime Date { get; set; }
        public int OpponentsScore { get; set; }
        public int TeamScore { get; set; }
        public string Location { get; set; }
        public PlayersGender PlayersGender { get; set; }
        public ICollection<MatchPlayer> Players { get; set; }
        public ICollection<MatchPoint> Points { get; set; }
        public ICollection<Card> Cards { get; set; }
        public Coach Coach { get; set; }
    }
}
