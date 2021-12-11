using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class MatchPlayerPerformanceDto
    {
        public Guid Id { get; set; }
        public MatchDto Match { get; set; }
        public PlayerDto Player { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public ICollection<MatchPlayerScoreDto> PlayerScores { get; set; }
    }
}
