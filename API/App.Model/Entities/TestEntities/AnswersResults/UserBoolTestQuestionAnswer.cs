using App.Model.Entities.TestEntities.QuestionTemplates;

namespace App.Model.Entities.TestEntities.AnswersResults
{
    public class UserBoolTestQuestionAnswer : BaseUserAnswer
    {
        public BoolTestQuestionTemplate Question { get; set; }
        public bool? Value { get; set; }
    }
}
