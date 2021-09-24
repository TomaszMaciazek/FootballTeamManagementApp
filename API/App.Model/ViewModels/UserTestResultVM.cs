using App.Model.Dtos;
using App.Model.Dtos.TestDtos;
using System.Collections.Generic;

namespace App.Model.ViewModels
{
    public class UserTestResultVM
    {
        public TestTemplateDto Test { get; set; }
        public UserDto User { get; set; }
        public ICollection<UserBoolTestQuestionAnswerDto> BoolSurveyQuestionsAnswers { get; set; }
        public ICollection<UserOptionsTestQuestionAnswerDto> OptionsSurveyQuestionsAnswers { get; set; }
    }
}
