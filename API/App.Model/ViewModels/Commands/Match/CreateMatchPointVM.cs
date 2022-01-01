using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateMatchPointVM
    {
        public Guid PlayerId { get; set; }
        public MatchPointType Point { get; set; }
        public int MinuteOfMatch { get; set; }
    }
}
