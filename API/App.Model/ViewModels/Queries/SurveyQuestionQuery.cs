using System;

namespace App.Model.ViewModels.Queries
{
    public class SurveyQuestionQuery
    {
        public Guid? SurveyId { get; set; }
        public int? SurveyPage { get; set; }
    }
}
