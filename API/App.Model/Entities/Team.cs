using App.Model.Entities.Common;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class Team : AuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
        public Coach MainCoach { get; set; }
    }
}
