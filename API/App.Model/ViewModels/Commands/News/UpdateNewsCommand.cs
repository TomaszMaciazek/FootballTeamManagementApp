using System;

namespace App.Model.ViewModels.Commands
{
    public class UpdateNewsCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? IsImportant { get; set; }
    }
}
