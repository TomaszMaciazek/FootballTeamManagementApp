using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class UserTestResultListItemDto
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string Title { get; set; }
        public SimpleUserDto Author { get; set; }
        public bool IsCompleated { get; set; }
    }
}
