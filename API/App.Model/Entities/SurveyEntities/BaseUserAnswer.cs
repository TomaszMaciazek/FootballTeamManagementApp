namespace App.Model.Entities.SurveyEntities
{
    public abstract class BaseUserAnswer : AuditableEntity
    {
        public User User { get; set; }
    }
}
