using App.Model.Enums;
using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class PlayerListItemDto
    {
        public Guid Id { get; set; }
        public SimpleTeamDto Team { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public int YearOfBirth { get; set; }
        public PlayerPosition PrefferedPosition { get; set; }
        public bool IsStillPlaying { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}
