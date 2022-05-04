using App.Model.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities
{
    public class UserTestResult : AuditableEntity
    {
        public User User { get; set; }
        public TestTemplate Test { get; set; }
        public ICollection<TestQuestionAnswer> QuestionAnswers { get; set; }
        [Required]
        public bool IsCompleated { get; set; }
        public bool? Passed { get; set; }
        public double? ScoredPoints { get; set; }
    }
}
