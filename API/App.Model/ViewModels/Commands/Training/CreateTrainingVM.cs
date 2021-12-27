using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateTrainingVM
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
