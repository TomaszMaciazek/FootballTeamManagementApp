using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class TrainingScoreListItemDto
    {
        public Guid Id { get; set; }
        public string PlayerName { get; set; }
        public DateTime LastModyfication { get; set; }
        public string LastModifier { get; set; }
    }
}
