using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class SurveyQuestionWithAnswersDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public IEnumerable<SurveyQuestionOptionDto> Options { get; set; }
        public IEnumerable<SurveyTextQuestionAnswerDto> TextQuestionAnswers { get; set; }
        public IEnumerable<SurveySelectQuestionAnswerDto> SelectQuestionAnswers { get; set; }
    }
}
