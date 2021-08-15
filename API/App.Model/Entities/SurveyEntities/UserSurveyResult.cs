namespace App.Model.Entities
{
    public class UserSurveyResult : AuditableEntity
    {
        public User User { get; set; }
        public SurveyTemplate Survey { get; set; }
        public bool IsCompleated { get; set; }
    }
}
