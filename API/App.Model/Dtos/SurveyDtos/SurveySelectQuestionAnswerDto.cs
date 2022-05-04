using System;

namespace App.Model.Dtos
{
    public class SurveySelectQuestionAnswerDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public int AnswerValue { get; set; }
    }
}
