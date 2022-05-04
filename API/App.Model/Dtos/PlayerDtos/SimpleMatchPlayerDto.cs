using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class SimpleMatchPlayerDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public bool IsActive { get; set; }
        public SimpleTeamDto Team { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
    }
}
