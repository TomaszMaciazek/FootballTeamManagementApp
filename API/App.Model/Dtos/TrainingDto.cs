using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class TrainingDto
    {
        public DateTime Date { get; set; }
        public ICollection<PlayerDto> Players { get; set; }
        public ICollection<CoachDto> Coaches { get; set; }
        public ICollection<TrainingScoreDto> Scores { get; set; }
        public string Description { get; set; }
        public string Localization { get; set; }
        public string Title { get; set; }
    }
}
