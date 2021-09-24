using App.Model.Dtos.Common;

namespace App.Model.Dtos.SurveyDtos
{
    public class UserRatingSurveyQuestionAnswerDto : AuditableEntityDto
    {
        public UserDto User { get; set; }
        public RatingSurveyQuestionTemplateDto Question { get; set; }
        public bool? Value { get; set; }
    }
}
