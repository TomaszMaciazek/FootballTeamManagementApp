using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserBoolSurveyQuestionAnswer : BaseUserAnswer
    {
        public BoolSurveyQuestionTemplate Question { get; set; }
        public bool? Value { get; set; }
    }
}
