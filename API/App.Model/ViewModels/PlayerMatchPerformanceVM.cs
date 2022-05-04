using App.Model.Dtos;
using App.Model.Enums;
using System.Collections.Generic;

namespace App.Model.ViewModels
{
    public class PlayerMatchPerformanceVM
    {
        public PlayerDto Player { get; set; }
        public MatchDto Match { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public IEnumerable<PlayerCardDto> Cards { get; set; }
        public IEnumerable<MatchPointDto> MatchPoints { get; set; }
    }
}
