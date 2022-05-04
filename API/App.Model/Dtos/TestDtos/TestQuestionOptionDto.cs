using System;

namespace App.Model.Dtos
{
    public class TestQuestionOptionDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public int Value { get; set; }
        public double Points { get; set; }
    }
}
