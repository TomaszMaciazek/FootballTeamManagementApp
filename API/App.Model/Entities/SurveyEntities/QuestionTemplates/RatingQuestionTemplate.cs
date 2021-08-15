using App.Model.Entities.SurveyEntities.AnswersResults;
using System.Collections.Generic;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class RatingQuestionTemplate : BaseSurveyQuestionTemplate
    {
        public int MaximalRate { get; set; }
        public ICollection<UserRatingQuestionAnswer> UsersAnswers { get; set; }
    }
}
