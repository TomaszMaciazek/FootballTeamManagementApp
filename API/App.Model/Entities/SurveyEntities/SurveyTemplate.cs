using App.Model.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveyTemplate : AuditableEntity
    {
        public ICollection<SurveyQuestion> Questions { get; set; }
        public ICollection<UserSurveyResult> RespondentsResults { get; set; }

        public User Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        public bool IsAnonymous { get; set; }
    }
}
