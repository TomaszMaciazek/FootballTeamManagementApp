using System;

namespace App.Model.ViewModels.Commands
{
    public class SelectQuestionAnswerVM
    {
        public Guid QuestionId { get; set; }
        public int? Value { get; set; }
    }
}
