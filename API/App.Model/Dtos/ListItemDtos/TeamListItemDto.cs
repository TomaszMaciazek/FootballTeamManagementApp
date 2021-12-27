using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class TeamListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SimpleCoachDto MainCoach { get; set; }
        public int MembersCount { get; set; }
        public bool IsActive { get; set; }
        public bool CanBeDeleted { get; set; }
    }
}
