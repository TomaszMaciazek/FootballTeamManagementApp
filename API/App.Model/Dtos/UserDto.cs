using System;

namespace App.Model.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string[] Roles { get; set; }
        public string[] Permissions { get; set; }
        public UserImageDto Image { get; set; }
        public bool IsActive { get; set; }
    }
}
