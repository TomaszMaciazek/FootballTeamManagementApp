using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class UserListItemDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public string UserName { get; set; }
        public RoleDto Role { get; set; }
        public Guid PlayerId { get; set; }
        public Guid CoachId { get; set; }
        public bool IsActive { get; set; }
    }
}
