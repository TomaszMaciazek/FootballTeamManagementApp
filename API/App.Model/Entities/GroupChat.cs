using App.Model.Entities.Common;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class GroupChat : BaseChat
    {
        public string Name { get; set; }
        public GroupChatImage Image { get; set; }
        public ICollection<GroupMessage> Messages { get; set; }
    }
}
