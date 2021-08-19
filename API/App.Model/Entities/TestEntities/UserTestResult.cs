namespace App.Model.Entities.TestEntities
{
    public class UserTestResult : AuditableEntity
    {
        public User User { get; set; }
        public TestTemplate Test { get; set; }
        public bool IsCompleated { get; set; }
        public decimal UserScore { get; set; }
    }
}
