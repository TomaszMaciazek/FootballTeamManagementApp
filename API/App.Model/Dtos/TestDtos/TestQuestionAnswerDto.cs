using System;

namespace App.Model.Dtos
{
    public class TestQuestionAnswerDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public int AnswerValue { get; set; }
    }
}
