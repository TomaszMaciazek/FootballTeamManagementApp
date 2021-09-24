using App.Model.Dtos.Common;
using App.Model.Enums.SurveyEnums;

namespace App.Model.Dtos.SurveyDtos
{
    public class TextSurveyQuestionTemplateDto : AuditableEntityDto
    {
        public TextQuestionType Type { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public int QuestionNumber { get; set; }
        public SurveyTemplateDto Survey { get; set; }
        public bool IsRequired { get; set; }
    }
}
