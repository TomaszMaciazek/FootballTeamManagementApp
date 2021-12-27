using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class SimplePlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
