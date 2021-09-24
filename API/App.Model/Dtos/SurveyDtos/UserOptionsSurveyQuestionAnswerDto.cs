using App.Model.Dtos.Common;

namespace App.Model.Dtos.SurveyDtos
{
    public class UserOptionsSurveyQuestionAnswerDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public OptionsSurveyQuestionTemplateDto Question { get; set; }
        public bool? Value { get; set; }
    }
}
