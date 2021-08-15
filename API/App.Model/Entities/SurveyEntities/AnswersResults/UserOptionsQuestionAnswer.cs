using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserOptionsQuestionAnswer : BaseUserAnswer
    {
        public OptionsQuestionTemplate Question { get; set; }
        public int? Value { get; set; }
    }
}
