using App.Model.Entities.SurveyEntities.AnswersResults;
using System.Collections.Generic;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class RatingSurveyQuestionTemplate : BaseSurveyQuestionTemplate
    {
        public int MaximalRate { get; set; }
        public ICollection<UserRatingSurveyQuestionAnswer> UsersAnswers { get; set; }
    }
}
