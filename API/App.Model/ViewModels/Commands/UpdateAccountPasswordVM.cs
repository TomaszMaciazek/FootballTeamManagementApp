using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateAccountPasswordVM
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
