using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Entities.SurveyEntities.AnswersTemplates;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class OptionsSurveyQuestionTemplate : BaseSurveyQuestionTemplate
    {
        [Required]
        public OptionQuestionType Type { get; set; }
        public ICollection<SurveyOptionQuestionAnswerTemplate> AnswerTemplates { get; set; }
        public ICollection<UserOptionsSurveyQuestionAnswer> UserAnswers { get; set; }
    }
}
