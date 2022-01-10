using App.Model.Enums;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateTestQuestionVM
    {
        public int Number { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateTestQuestionOptionVM> Options { get; set; }
    }
}
