using App.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveyQuestionOption : EditableEntity
    {
        [Required]
        public string Label { get; set; }
        public int Value { get; set; }
        public SurveyQuestion Question { get; set; }
    }
}
