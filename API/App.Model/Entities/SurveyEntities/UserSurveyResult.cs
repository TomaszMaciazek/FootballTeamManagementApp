using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class UserSurveyResult : AuditableEntity
    {
        public User User { get; set; }
        public SurveyTemplate Survey { get; set; }

        [Required]
        public bool IsCompleated { get; set; }
        public ICollection<SurveyTextQuestionAnswer> TextQuestionAnswers { get; set; }
        public ICollection<SurveySelectQuestionAnswer> SelectQuestionAnswers { get; set; }
    }
}
