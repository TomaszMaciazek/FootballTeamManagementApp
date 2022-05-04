using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateUserCommandVM
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int? BadLogonCount { get; set; }
        public DateTime? AccountLockoutTime { get; set; }
        public DateTime? LastBadPasswordAttempt { get; set; }
        public DateTime? LastLogon { get; set; }
        public DateTime? LastPasswordSet { get; set; }
        public bool? IsActive { get; set; }
    }
}
