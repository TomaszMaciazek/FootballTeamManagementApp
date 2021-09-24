using App.Model.Dtos.Common;

namespace App.Model.Dtos.TestDtos
{
    public class UserOptionsTestQuestionAnswerDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public OptionsTestQuestionTemplateDto Question { get; set; }
        public int? Value { get; set; }
    }
}
