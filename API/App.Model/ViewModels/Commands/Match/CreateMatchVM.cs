using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateMatchVM
    {
        public DateTime Date { get; set; }
        public string OpponentsClubName { get; set; }
        public int OpponentsScore { get; set; }
        public int ClubScore { get; set; }
        public string Location { get; set; }
        public PlayersGender PlayersGender { get; set; }
        public MatchType MatchType { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateMatchPlayerPerformanceVM> PlayerPerformances { get; set; }
        public IEnumerable<CreateMatchPointVM> MatchPoints { get; set; }
        public IEnumerable<CreateMatchPlayerCardVM> PlayersCards { get; set; }
        public IEnumerable<CreateMatchCoachCardVM> CoachesCards { get; set; }
    }
}
