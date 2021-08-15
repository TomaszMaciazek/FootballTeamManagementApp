using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class OptionsQuestionTemplate : BaseSurveyQuestionTemplate
    {
        public OptionQuestionType Type { get; set; }
        public ICollection<UserOptionsQuestionAnswer> UserAnswers { get; set; }
    }
}
