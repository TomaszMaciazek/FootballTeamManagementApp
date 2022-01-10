using App.Model.Entities.Common;

namespace App.Model.Entities
{
    public class MessageRecipient : EditableEntity
    {
        public Message Message { get; set; }
        public User Recipient { get; set; }
    }
}
