﻿using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
    }
}
