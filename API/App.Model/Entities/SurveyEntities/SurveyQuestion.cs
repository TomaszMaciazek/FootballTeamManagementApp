using App.Model.Entities.Common;
using App.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveyQuestion : EditableEntity
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRequired { get; set; }
        public SurveyTemplate Survey { get; set; }
        public ICollection<SurveyQuestionOption> Options { get; set; }
        public ICollection<SurveySelectQuestionAnswer> SelectQuestionAnswers { get; set; }
        public ICollection<SurveyTextQuestionAnswer> TextQuestionAnswers { get; set; }

    }
}
