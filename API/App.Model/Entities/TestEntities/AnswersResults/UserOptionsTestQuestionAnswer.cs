using App.Model.Entities.TestEntities.QuestionTemplates;

namespace App.Model.Entities.TestEntities.AnswersResults
{
    public class UserOptionsTestQuestionAnswer : BaseUserAnswer
    {
        public OptionsTestQuestionTemplate Question { get; set; }
        public int? Value { get; set; }
    }
}
