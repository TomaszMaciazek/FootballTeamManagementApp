using App.Model.Enums;
using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class MatchListItemDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string OpponentsClubName { get; set; }
        public int OpponentsScore { get; set; }
        public int ClubScore { get; set; }
        public PlayersGender PlayersGender { get; set; }
        public MatchType MatchType { get; set; }
        public bool IsActive { get; set; }
    }
}
