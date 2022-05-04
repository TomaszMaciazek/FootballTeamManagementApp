using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class SurveyWithResultsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAnonymous { get; set; }
        public IEnumerable<SurveyQuestionWithAnswersDto> Questions { get; set; }
    }
}
