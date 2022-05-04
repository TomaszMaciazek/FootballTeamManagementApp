using System;

namespace App.Model.Dtos
{
    public class SimpleSurveyResultDto
    {
        public Guid Id { get; set; }
        public bool IsCompleated { get; set; }
        public SimpleUserDto Respondent { get; set; }
    }
}
