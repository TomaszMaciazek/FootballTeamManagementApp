using System;

namespace App.Model.Dtos
{
    public class SelectUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public RoleDto Role { get; set; }
        public SelectUserPlayerDetailsDto Player { get; set; }
        public SelectUserCoachDetailsDto Coach { get; set; }
    }
}
