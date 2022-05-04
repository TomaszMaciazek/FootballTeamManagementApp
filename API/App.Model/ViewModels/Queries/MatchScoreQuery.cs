using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Queries
{
    public class MatchScoreQuery
    {
        public Guid MatchId { get; set; }
        public IEnumerable<MatchScoreType> ScoreTypes { get; set; }
    }
}
