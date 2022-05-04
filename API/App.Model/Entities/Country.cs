using System.Collections.Generic;

namespace App.Model.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Coach> Coaches { get; set; }
    }
}
