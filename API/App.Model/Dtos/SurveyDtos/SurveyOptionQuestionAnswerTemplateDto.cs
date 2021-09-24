using App.Model.Dtos.Common;

namespace App.Model.Dtos.SurveyDtos
{
    public class SurveyOptionQuestionAnswerTemplateDto : AuditableEntityDto
    {
        public OptionsSurveyQuestionTemplateDto Question { get; set; }
        public int Value { get; set; }
        public string Content { get; set; }
        public int QuestionNumber { get; set; }
    }
}
