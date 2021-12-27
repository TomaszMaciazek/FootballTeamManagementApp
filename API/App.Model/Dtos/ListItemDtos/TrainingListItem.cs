using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class TrainingListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }
}
