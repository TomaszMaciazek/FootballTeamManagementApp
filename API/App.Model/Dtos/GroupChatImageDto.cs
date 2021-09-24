using App.Model.Dtos.Common;

namespace App.Model.Dtos
{
    public class GroupChatImageDto : AuditableEntityDto
    {
        public GroupChatDto Chat { get; set; }
        public byte[] Data { get; set; }
    }
}
