using System;

namespace App.Model.Dtos.ListItemDtos
{
    public class SimpleUserTestResultListItemDto
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public SimpleUserDto Respondent { get; set; }
        public bool IsCompleated { get; set; }
        public bool? Passed { get; set; }
        public double? ScoredPoints { get; set; }
    }
}
