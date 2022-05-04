using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class MyTestListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int RespondentsCount { get; set; }
        public int NumberOfCompleatedResults { get; set; }
    }
}
