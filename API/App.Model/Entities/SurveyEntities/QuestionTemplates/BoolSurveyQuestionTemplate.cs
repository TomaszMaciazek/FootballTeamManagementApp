using App.Model.Entities.SurveyEntities.AnswersResults;
using System.Collections.Generic;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class BoolSurveyQuestionTemplate : BaseSurveyQuestionTemplate
    {
        public ICollection<UserBoolSurveyQuestionAnswer> UserAnswers { get; set; }
    }
}
