using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserRatingSurveyQuestionAnswer : BaseUserAnswer
    {
        public RatingSurveyQuestionTemplate Question { get; set; }
        public int? Value { get; set; }
    }
}
