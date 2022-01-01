using System;

namespace App.Model.Dtos
{
    public class SimpleSelectPlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public SimpleTeamDto Team { get; set; }
    }
}
