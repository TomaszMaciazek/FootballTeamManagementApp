using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateMatchPlayerScoreVM
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
    }
}
