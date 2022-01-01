using System;

namespace App.Model.Dtos
{
    public class UserAccountDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPasswordSet { get; set; }
        public PlayerAccountDto Player { get; set; }
        public CoachAccountDto Coach { get; set; }
        public RoleDto Role { get; set; }
    }
}
