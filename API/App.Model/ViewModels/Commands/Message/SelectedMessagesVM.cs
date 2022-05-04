using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class SelectedMessagesVM
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> MessagesIds { get; set; }
    }
}
