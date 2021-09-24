using System;
using System.Collections.Generic;

namespace App.ServiceLayer.Queries
{
    public class SurveyAnswerQuery
    {
        public IEnumerable<Guid> QuestionsIds { get; set; }
        public Guid? UserId { get; set; }
    }
}
