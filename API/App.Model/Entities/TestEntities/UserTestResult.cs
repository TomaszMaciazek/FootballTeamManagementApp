using App.Model.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities
{
    public class UserTestResult : AuditableEntity
    {
        public User User { get; set; }
        public TestTemplate Test { get; set; }

        [Required]
        public bool IsCompleated { get; set; }

        [Required]
        public double UserScore { get; set; }
    }
}
