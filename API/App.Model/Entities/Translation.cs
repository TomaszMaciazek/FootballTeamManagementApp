using App.Model.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Translation : AuditableEntity
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public Language Language { get; set; }
    }
}
