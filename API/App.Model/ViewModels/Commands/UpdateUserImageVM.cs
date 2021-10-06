using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateUserImageVM
    {
        public Guid UserId { get; set; }
        public byte[] ImageData { get; set; }
    }
}
