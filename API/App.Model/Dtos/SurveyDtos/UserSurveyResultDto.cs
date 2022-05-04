using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class UserSurveyResultDto
    {
        public Guid Id { get; set; }
        public SimpleSurveyTemplateDto Survey { get; set; }
        public bool IsCompleated { get; set; }
        public ICollection<SurveyTextQuestionAnswerDto> TextQuestionAnswers { get; set; }
        public ICollection<SurveySelectQuestionAnswerDto> SelectQuestionAnswers { get; set; }

    }
}
