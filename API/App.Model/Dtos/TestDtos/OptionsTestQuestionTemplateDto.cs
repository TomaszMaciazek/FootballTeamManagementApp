using App.Model.Dtos.Common;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;

namespace App.Model.Dtos.TestDtos
{
    public class OptionsTestQuestionTemplateDto : AuditableEntityDto
    {
        public TestTemplateDto Test { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public int QuestionNumber { get; set; }
        public double PointsToEarn { get; set; }
        public OptionQuestionType Type { get; set; }
        public ICollection<TestOptionQuestionAnswerTemplateDto> Answers { get; set; }
    }
}
