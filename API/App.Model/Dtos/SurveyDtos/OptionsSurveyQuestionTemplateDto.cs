using App.Model.Dtos.Common;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;

namespace App.Model.Dtos.SurveyDtos
{
    public class OptionsSurveyQuestionTemplateDto : AuditableEntityDto
    {
        public SurveyTemplateDto Survey { get; set; }
        public OptionQuestionType Type { get; set; }
        public ICollection<SurveyOptionQuestionAnswerTemplateDto> AnswerTemplates { get; set; }
        public bool IsRequired { get; set; }
    }
}
