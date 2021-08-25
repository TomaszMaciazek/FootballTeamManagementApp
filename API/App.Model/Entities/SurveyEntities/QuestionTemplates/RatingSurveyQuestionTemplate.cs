using App.Model.Entities.SurveyEntities.AnswersResults;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class RatingSurveyQuestionTemplate : BaseSurveyQuestionTemplate
    {
        [Required]
        public int MaximalRate { get; set; }
        public ICollection<UserRatingSurveyQuestionAnswer> UsersAnswers { get; set; }
    }
}
