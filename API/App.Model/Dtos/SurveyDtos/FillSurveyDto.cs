using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class FillSurveyDto
    {
        public Guid Id { get; set; }
        public IEnumerable<SurveyQuestionDto> Questions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
