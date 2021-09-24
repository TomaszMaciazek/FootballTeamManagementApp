using App.Model.Dtos.Common;
using System.Collections.Generic;

namespace App.Model.Dtos.TestDtos
{
    public class TestTemplateDto : AuditableEntityDto
    { 
        public UserDto Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PagesCount { get; set; }
        public ICollection<BoolTestQuestionTemplateDto> BoolTestQuestions { get; set; }
        public ICollection<OptionsTestQuestionTemplateDto> OptionsTestQuestions { get; set; }
    }
}
