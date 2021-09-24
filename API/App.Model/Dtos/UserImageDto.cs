using App.Model.Dtos.Common;

namespace App.Model.Dtos
{
    public class UserImageDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public byte[] Data { get; set; }
    }
}
