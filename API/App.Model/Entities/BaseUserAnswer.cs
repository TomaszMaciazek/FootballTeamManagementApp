namespace App.Model.Entities
{
    public abstract class BaseUserAnswer : AuditableEntity
    {
        public User User { get; set; }
    }
}
