using System;

namespace App.Model.ViewModels.Commands
{
    public class RemovePlayerFromTeam
    {
        public Guid PlayerId { get; set; }
        public Guid TeamId { get; set; }
    }
}
