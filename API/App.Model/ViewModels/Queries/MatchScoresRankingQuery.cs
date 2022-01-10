using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Queries
{
    public class MatchScoresRankingQuery
    {
        public MatchScoreType ScoreType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? OnlyStillPlaying { get; set; }
        public int Count { get; set; }
        public IEnumerable<Guid> TeamsIds { get; set; }
        public bool GetPlayersWithoutTeam { get; set; }
    }
}
