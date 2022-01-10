using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class FillTestVM
    {
        public Guid RespondentId { get; set; }
        public Guid TestId { get; set; }
        public IEnumerable<TestQuestionAnswerVM> QuestionAnswers { get; set; }
    }
}
