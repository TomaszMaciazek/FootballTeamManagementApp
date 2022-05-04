using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class FillSurveyVM
    {
        public Guid RespondentId { get; set; }
        public Guid SurveyId { get; set; }
        public IEnumerable<TextQuestionAnswerVM> TextQuestionAnswers { get; set; }
        public IEnumerable<SelectQuestionAnswerVM> SelectQuestionAnswers { get; set; }
    }
}
