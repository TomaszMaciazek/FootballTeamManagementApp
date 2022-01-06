using App.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities
{
    public class TestQuestionOption : EditableEntity
    {
        [Required]
        public string Label { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        [Range(-1, 1, ErrorMessage = "The score must be in range <-1,1>")]
        public double Points { get; set; }
        public TestQuestion Question { get; set; }
    }
}
