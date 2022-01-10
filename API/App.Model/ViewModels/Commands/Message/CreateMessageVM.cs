using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateMessageVM
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime? SendDate { get; set; }
        public IEnumerable<Guid> RecipientsIds { get; set; }
        public Guid SenderId { get; set; }
    }
}
