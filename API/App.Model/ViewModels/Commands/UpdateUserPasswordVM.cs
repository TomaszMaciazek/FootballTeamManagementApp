using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateUserPasswordVM
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
