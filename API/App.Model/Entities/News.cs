using App.Model.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class News : AuditableEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
        public bool IsImportant { get; set; }
    }
}
