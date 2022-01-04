using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class SurveyQuestionDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public IEnumerable<SurveyQuestionOptionDto> Options { get; set; }
    }
}
