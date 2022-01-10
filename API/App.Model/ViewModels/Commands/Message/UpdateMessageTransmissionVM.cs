using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateMessageTransmissionVM
    {
        public Guid Id { get; set; }
        public MailboxType? MailboxType { get; set; }
        public bool? IsActive { get; set; }
    }
}
