using App.Model.Enums;
using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class CoachListItemDto
    {
        public Guid Id { get; set; }
        public int TeamsCount { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public bool IsStillWorking { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}
