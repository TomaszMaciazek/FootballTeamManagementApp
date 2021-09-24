using App.Model.Dtos;
using App.Model.Dtos.SurveyDtos;
using System;
using System.Collections.Generic;

namespace App.Model.ViewModels
{
    public class UserSurveyResultVM
    {
        public Guid Id { get; set; }
        public SurveyTemplateDto Survey { get; set; }
        public UserDto User { get; set; }
        public ICollection<UserBoolSurveyQuestionAnswerDto> BoolSurveyQuestionsAnswers { get; set; }
        public ICollection<UserOptionsSurveyQuestionAnswerDto> OptionsSurveyQuestionsAnswers { get; set; }
        public ICollection<UserRatingSurveyQuestionAnswerDto> RatingSurveyQuestionsAnswers { get; set; }
        public ICollection<UserTextSurveyQuestionAnswerDto> UserTextSurveyQuestionsAnswers { get; set; }
        public bool IsCompleated { get; set; }
    }
}
