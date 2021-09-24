using App.Model.Dtos.Common;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class IndividualChatDto : AuditableEntityDto
    {
        public bool IsHidden { get; set; }
        public ICollection<UserDto> Users { get; set; }
        public ICollection<IndividualMessageDto> Messages { get; set; }
    }
}
