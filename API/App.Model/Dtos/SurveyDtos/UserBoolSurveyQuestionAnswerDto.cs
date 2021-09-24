using App.Model.Dtos.Common;

namespace App.Model.Dtos.SurveyDtos
{
    public class UserBoolSurveyQuestionAnswerDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public BoolSurveyQuestionTemplateDto Question { get; set; }
        public bool? Value { get; set; }
    }
}
