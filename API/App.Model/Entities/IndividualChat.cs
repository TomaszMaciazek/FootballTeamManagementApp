using App.Model.Entities.Common;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class IndividualChat : BaseChat
    {
        public ICollection<IndividualMessage> Messages { get; set; }
    }
}
