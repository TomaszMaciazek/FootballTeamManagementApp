using System;

namespace App.Model.Dtos
{
    public class SimpleUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
    }
}
