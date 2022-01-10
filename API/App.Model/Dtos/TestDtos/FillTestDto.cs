using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class FillTestDto
    {
        public Guid Id { get; set; }
        public IEnumerable<FillTestQuestionDto> Questions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double PassedMinimalValue { get; set; }
    }
}
