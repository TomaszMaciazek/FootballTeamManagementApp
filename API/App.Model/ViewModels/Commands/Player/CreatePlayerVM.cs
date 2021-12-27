using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Commands
{
    public class CreatePlayerVM : CreateUserVM
    {
        public DateTime BirthDate { get; set; }
        public int CountryId { get; set; }
        public DateTime? StartedPlaying { get; set; }
        public Gender Gender { get; set; }
        public PlayerPosition PrefferedPosition { get; set; }
        public Guid? TeamId { get; set; }
    }
}
