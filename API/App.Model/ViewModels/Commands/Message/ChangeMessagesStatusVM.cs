using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class ChangeMessagesStatusVM
    {
        public Guid UserId { get; set; }
        public bool IsRead { get; set; }
        public IEnumerable<Guid> MessagesIds { get; set; }
    }
}
