using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserTextSurveyQuestionAnswer : BaseUserAnswer
    {
        public TextSurveyQuestionTemplate Question { get; set; }
        public string Value { get; set; }
    }
}
