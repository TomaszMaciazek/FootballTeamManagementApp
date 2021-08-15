using System.Collections.Generic;

namespace App.Model.Entities
{
    public class GroupChat : Chat
    {
        public string Name { get; set; }
        public GroupChatImage Image { get; set; }
        public ICollection<GroupMessage> Messages { get; set; }
    }
}
