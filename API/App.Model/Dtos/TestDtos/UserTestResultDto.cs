using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class UserTestResultDto
    {
        public Guid Id { get; set; }
        public SimpleTestTemplateDto Test { get; set; }
        public bool IsCompleated { get; set; }
        public bool? Passed { get; set; }
        public double? ScoredPoints { get; set; }
        public ICollection<TestQuestionAnswerDto> QuestionAnswers { get; set; }
    }
}
