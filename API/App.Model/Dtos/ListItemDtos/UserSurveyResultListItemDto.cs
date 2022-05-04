using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class UserSurveyResultListItemDto
    {
        public Guid Id { get; set; }
        public Guid SurveyId { get; set; }
        public string Title { get; set; }
        public SimpleUserDto Author { get; set; }
        public bool IsCompleated { get; set; }
    }
}
