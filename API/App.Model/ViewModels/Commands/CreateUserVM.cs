using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateUserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public byte[] ImageData { get; set; }
    }
}
