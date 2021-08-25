using App.Model.Entities.Common;
using App.Model.Entities.TestEntities.QuestionTemplates;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities.AnswersTemplates
{
    public class TestOptionQuestionAnswerTemplate : AuditableEntity
    {
        public OptionsTestQuestionTemplate Question { get; set; }

        [Required]
        public int Value { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public decimal PointsForAnswer { get; set; }
    }
}
