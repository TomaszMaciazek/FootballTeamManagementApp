using System.Collections.Generic;

namespace App.Model.Entities
{
    public class IndividualChat : Chat
    {
        public ICollection<IndividualMessage> Messages { get; set; }
    }
}
