using App.Model.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities
{
    public class TestTemplate : AuditableEntity
    {
        public User Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        [Required]
        public double PassedMinimalValue { get; set; }
        public ICollection<TestQuestion> Questions { get; set; }
        public ICollection<UserTestResult> UserResults { get; set; }
    }
}
