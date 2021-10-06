﻿using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreatePlayerVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public Gender Gender { get; set; }
        public PlayerPosition PrefferedPosition { get; set; }
    }
}