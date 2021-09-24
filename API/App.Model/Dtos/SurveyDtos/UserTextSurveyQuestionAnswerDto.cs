using App.Model.Dtos.Common;

namespace App.Model.Dtos.SurveyDtos
{
    public class UserTextSurveyQuestionAnswerDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public TextSurveyQuestionTemplateDto Question { get; set; }
        public bool? Value { get; set; }
    }
}
