using App.Model.Dtos.Common;
using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class MatchDto : AuditableEntityDto
    {
        public DateTime Date { get; set; }
        public int? OpponentsScore { get; set; }
        public string Location { get; set; }
        public PlayersGender PlayersGender { get; set; }
        public bool IsModificationForbidden { get; set; }
    }
}
