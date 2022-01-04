using App.Model.Enums;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateSurveyQuestionVM
    {
        public int Number { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public IEnumerable<CreateSurveyQuestionOptionVM> Options { get; set; }
    }
}
