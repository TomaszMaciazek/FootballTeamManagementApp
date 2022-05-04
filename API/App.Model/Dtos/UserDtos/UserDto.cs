using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public RoleDto Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserDetailsDto : UserDto
    {
        public string PlayerTeamName { get; set; }
        public Gender? Gender { get; set; }
        public PlayerPosition PlayerPrefferedPosition { get; set; }
        public DateTime? PlayerStartedPlaying { get; set; }
        public DateTime? PlayerFinishedPlaying { get; set; }
        public DateTime? CoachStartedWorking { get; set; }
        public DateTime? CoachFinishedWorking { get; set; }
        public DateTime? BirthDate { get; set; }
        public int CountryId { get; set; }
    }
}
