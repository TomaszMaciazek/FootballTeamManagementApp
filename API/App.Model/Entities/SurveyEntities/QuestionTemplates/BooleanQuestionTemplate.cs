using App.Model.Entities.SurveyEntities.AnswersResults;
using System.Collections.Generic;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class BooleanQuestionTemplate : BaseSurveyQuestionTemplate
    {
        public ICollection<UserBooleanQuestionAnswer> UserAnswers { get; set; }
    }
}
