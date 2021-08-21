using App.Model.Entities.Common;

namespace App.Model.Entities
{
    public class GroupMessage : BaseMessage
    {
        public GroupChat Chat { get; set; }
    }
}
