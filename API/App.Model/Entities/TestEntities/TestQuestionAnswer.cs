using App.Model.Entities.Common;

namespace App.Model.Entities.TestEntities
{
    public class TestQuestionAnswer : EditableEntity
    {
        public TestQuestion Question { get; set; }
        public UserTestResult UserResult { get; set; }
        public int? AnswerValue { get; set; }
    }
}
