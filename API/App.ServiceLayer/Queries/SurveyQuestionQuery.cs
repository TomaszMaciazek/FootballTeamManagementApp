using System;

namespace App.ServiceLayer.Queries
{
    public class SurveyQuestionQuery
    {
        public Guid? SurveyId { get; set; }
        public int? SurveyPage { get; set; }
    }
}
