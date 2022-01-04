using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class SurveyListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public SimpleUserDto Author { get; set; }
        public int RespondentsCount { get; set; }
        public int NumberOfCompleatedResults { get; set; }
    }
}
