using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.Common
{
    public abstract class BaseQuestionSetEntity : AuditableEntity
    {
        public User Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

    }
}
