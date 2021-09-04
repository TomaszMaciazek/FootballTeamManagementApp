using App.Model.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class GroupChat : BaseChat
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(256)]
        public string Name { get; set; }
        public GroupChatImage Image { get; set; }
        public ICollection<GroupMessage> Messages { get; set; }
        public ICollection<User> ChatOwners { get; set; }
    }
}
