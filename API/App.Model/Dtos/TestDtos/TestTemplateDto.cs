using App.Model.Dtos.Common;
using System;
using System.Collections.Generic;

namespace App.Model.Dtos
{
    public class TestTemplateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TestQuestionDto> Questions { get; set; }
    }
}
