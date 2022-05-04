using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands.MatchPoint
{
    public class CreateMatchPointVM
    {
        public Guid MatchId { get; set; }
        public Guid PlayerId { get; set; }
        public MatchPointType Point { get; set; }
        public int MinuteOfMatch { get; set; }
    }
}
