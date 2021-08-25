using App.Model.Entities.SurveyEntities.AnswersResults;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities.QuestionTemplates
{
    public class TextSurveyQuestionTemplate : BaseSurveyQuestionTemplate
    {
        [Required]
        public TextQuestionType Type { get; set; }
        public ICollection<UserTextSurveyQuestionAnswer> QuestionAnswers { get; set; }
    }
}
