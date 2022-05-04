using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateMatchPlayerPerformanceVM
    {
        public Guid PlayerId { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
    }
}
