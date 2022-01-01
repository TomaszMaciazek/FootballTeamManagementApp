using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class SimpleMatchDto
    {
        public Guid Id { get; set; }
        public string OpponentsClubName { get; set; }
        public DateTime Date { get; set; }
        public MatchType MatchType { get; set; }
    }
}
