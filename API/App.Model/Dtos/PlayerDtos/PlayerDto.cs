using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public DateTime? FinishedPlaying { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public PlayerPosition PrefferedPosition { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public SimpleTeamDto Team { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public int MatchCount { get; set; }
    }
}
