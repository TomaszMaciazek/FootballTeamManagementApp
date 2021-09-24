using App.Model.Dtos.Common;
using System.Collections.Generic;

namespace App.Model.Dtos.SurveyDtos
{
    public class SurveyTemplateDto : AuditableEntityDto
    {
        public UserDto Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PagesCount { get; set; }
        public ICollection<TextSurveyQuestionTemplateDto> TextQuestionTemplates { get; set; }
        public ICollection<OptionsSurveyQuestionTemplateDto> OptionsQuestionTemplates { get; set; }
        public ICollection<BoolSurveyQuestionTemplateDto> BoolQuestionTemplates { get; set; }
        public ICollection<RatingSurveyQuestionTemplateDto> RatingQuestionTemplates { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
