using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities.QuestionTemplates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveyTemplate : BaseQuestionSetEntity
    {
        public ICollection<TextSurveyQuestionTemplate> TextQuestionTemplates { get; set; }
        public ICollection<OptionsSurveyQuestionTemplate> OptionsQuestionTemplates { get; set; }
        public ICollection<BoolSurveyQuestionTemplate> BoolQuestionTemplates { get; set; }
        public ICollection<RatingSurveyQuestionTemplate> RatingQuestionTemplates { get; set; }
        public ICollection<UserSurveyResult> RespondentsResults { get; set; }

        [Required]
        public bool IsAnonymous { get; set; }
    }
}
