using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdatePasswordVM
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
