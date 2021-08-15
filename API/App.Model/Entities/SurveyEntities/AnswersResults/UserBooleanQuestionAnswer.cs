using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserBooleanQuestionAnswer : BaseUserAnswer
    {
        public BooleanQuestionTemplate Question { get; set; }
        public bool? Value { get; set; }
    }
}
