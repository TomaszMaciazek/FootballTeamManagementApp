using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class TestQuestionDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public IEnumerable<TestQuestionOptionDto> Options { get; set; }
    }
}
