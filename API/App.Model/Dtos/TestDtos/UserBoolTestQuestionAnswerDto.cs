using App.Model.Dtos.Common;

namespace App.Model.Dtos.TestDtos
{
    public class UserBoolTestQuestionAnswerDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public BoolTestQuestionTemplateDto Question { get; set; }
        public bool? Value { get; set; }
    }
}
