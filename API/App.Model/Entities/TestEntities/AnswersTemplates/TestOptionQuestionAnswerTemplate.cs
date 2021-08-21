using App.Model.Entities.Common;
using App.Model.Entities.TestEntities.QuestionTemplates;

namespace App.Model.Entities.TestEntities.AnswersTemplates
{
    public class TestOptionQuestionAnswerTemplate : AuditableEntity
    {
        public OptionsTestQuestionTemplate Question { get; set; }
        public int Value { get; set; }
        public string Content { get; set; }
        public int QuestionNumber { get; set; }
        public bool IsCorrect { get; set; }
        public decimal PointsForAnswer { get; set; }
    }
}
