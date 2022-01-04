using System;

namespace App.Model.Dtos
{
    public class SurveyTextQuestionAnswerDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public string AnswerValue { get; set; }
    }
}
