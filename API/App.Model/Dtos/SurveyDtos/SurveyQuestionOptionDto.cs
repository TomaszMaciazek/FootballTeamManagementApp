using System;

namespace App.Model.Dtos
{
    public class SurveyQuestionOptionDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
