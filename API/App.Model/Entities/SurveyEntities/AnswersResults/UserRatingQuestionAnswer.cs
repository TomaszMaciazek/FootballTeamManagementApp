using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserRatingQuestionAnswer : BaseUserAnswer
    {
        public RatingQuestionTemplate Question { get; set; }
        public int? Value { get; set; }
    }
}
