using System;

namespace App.Model.Dtos
{
    public class SimpleSurveyTemplateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
