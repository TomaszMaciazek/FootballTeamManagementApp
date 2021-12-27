using System;

namespace App.Model.Dtos
{
    public class SimpleCoachDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
    }
}
