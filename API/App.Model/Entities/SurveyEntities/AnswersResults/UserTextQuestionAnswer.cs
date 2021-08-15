using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserTextQuestionAnswer : BaseUserAnswer
    {
        public TextQuestionTemplate Question { get; set; }
        public string Value { get; set; }
    }
}
