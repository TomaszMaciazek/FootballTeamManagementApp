using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.Common
{
    public abstract class BaseQuestionTemplate : AuditableEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int QuestionNumber { get; set; }
    }
}
