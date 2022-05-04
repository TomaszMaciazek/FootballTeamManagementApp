using System;
using System.Collections.Generic;

namespace App.Model.ViewModels.Queries
{
    public class TestAnswerQuery
    {
        public IEnumerable<Guid> QuestionsIds { get; set; }
        public Guid? UserId { get; set; }
    }
}
