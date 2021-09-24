using App.Model.Dtos.Common;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class GroupChatDto : AuditableEntityDto
    {
        public string Name { get; set; }
        public GroupChatImageDto Image { get; set; }
        public bool IsHidden { get; set; }
        public ICollection<UserDto> Users { get; set; }
        public ICollection<GroupMessageDto> Messages { get; set; }
        public ICollection<UserDto> ChatOwners { get; set; }
    }
}
