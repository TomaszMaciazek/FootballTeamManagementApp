using App.Model.Dtos.Common;
using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class PlayerDto : AuditableEntityDto
    {
        public Guid UserId { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public DateTime? FinishedPlaying { get; set; }
        public Gender Gender { get; set; }
        public PlayerPosition PrefferedPosition { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public TeamDto Team { get; set; }
    }
}
