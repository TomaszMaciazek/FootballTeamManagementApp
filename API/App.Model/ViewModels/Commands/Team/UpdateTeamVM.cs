using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateTeamVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? MainCoachId { get; set; }
    }
}
