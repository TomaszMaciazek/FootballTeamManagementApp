namespace App.Model.Entities.Common
{
    public abstract class BaseUserAnswer : AuditableEntity
    {
        public User User { get; set; }
    }
}
