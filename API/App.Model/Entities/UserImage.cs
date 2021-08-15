namespace App.Model.Entities
{
    public class UserImage : AuditableEntity
    {
        public User User { get; set; }
        public byte[] Data { get; set; }
    }
}
