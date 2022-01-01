using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveyQuestionOption
    {
        public Guid Id { get; set; }
        [Required]
        public string Label { get; set; }
        public int Value { get; set; }
        public SurveyQuestion Question { get; set; }
    }
}
