using App.Model.Entities.Common;

namespace App.Model.Entities
{
    public class IndividualMessage : BaseMessage
    {
        public IndividualChat Chat { get; set; }
    }
}
