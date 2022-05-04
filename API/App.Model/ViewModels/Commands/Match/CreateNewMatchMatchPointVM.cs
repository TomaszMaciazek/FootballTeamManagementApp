using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateNewMatchMatchPointVM
    {
        public Guid PlayerId { get; set; }
        public MatchPointType Point { get; set; }
        public int MinuteOfMatch { get; set; }
    }
}
