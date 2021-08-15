using App.Model.Entities.SurveyEntities.QuestionTemplates;
using System;
using System.Collections.Generic;

namespace App.Model.Entities
{
    public class SurveyTemplate : AuditableEntity
    {
        public User Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public ICollection<TextQuestionTemplate> TextQuestionTemplates { get; set; }
        public ICollection<OptionsQuestionTemplate> OptionsQuestionTemplates { get; set; }
        public ICollection<BooleanQuestionTemplate> BooleanQuestionTemplates { get; set; }
        public ICollection<RatingQuestionTemplate> RatingQuestionTemplates { get; set; }
        public ICollection<UserSurveyResult> Respondents { get; set; }
    }
}
