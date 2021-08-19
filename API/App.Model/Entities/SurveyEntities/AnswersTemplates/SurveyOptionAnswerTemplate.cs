using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersTemplates
{
    public class SurveyOptionAnswerTemplate : AuditableEntity
    {
        public OptionsSurveyQuestionTemplate Question { get; set; }
        public int Value { get; set; }
        public string Content { get; set; }
        public int QuestionNumber { get; set; }
    }
}
