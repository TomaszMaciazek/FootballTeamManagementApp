using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class AddPlayersToTeamVM
    {
        public IEnumerable<Guid> PlayersIds { get; set; }
        public Guid TeamId { get; set; }
    }
}
