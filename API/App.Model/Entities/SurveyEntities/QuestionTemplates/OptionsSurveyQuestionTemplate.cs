using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class OptionsSurveyQuestionTemplate : BaseSurveyQuestionTemplate
    {
        public OptionQuestionType Type { get; set; }
        public ICollection<UserOptionsSurveyQuestionAnswer> UserAnswers { get; set; }
    }
}
