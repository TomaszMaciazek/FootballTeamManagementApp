using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateTeamVM
    {
        public string Name { get; set; }
        public Guid? MainCoachId { get; set; }
    }
}
