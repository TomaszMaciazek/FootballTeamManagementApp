using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateTestVM
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double PassedMinimalValue { get; set; }
        public IEnumerable<CreateTestQuestionVM> Questions { get; set; }
        public IEnumerable<Guid> RespondentsIds { get; set; }
    }
}
