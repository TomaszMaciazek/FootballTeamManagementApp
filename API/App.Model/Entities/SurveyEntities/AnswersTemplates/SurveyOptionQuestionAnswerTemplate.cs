using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities.AnswersTemplates
{
    public class SurveyOptionQuestionAnswerTemplate : AuditableEntity
    {
        public OptionsSurveyQuestionTemplate Question { get; set; }

        [Required]
        public int Value { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        [Required]
        public int QuestionNumber { get; set; }
    }
}
