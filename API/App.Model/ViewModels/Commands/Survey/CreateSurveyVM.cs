using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Commands
{
    public class CreateSurveyVM
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAnonymous { get; set; }
        public IEnumerable<CreateSurveyQuestionVM> Questions { get; set; }
        public IEnumerable<Guid> RespondentsIds { get; set; }
    }
}
