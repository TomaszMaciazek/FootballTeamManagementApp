namespace App.Model.Entities
{
    public abstract class BaseQuestionAswerTemplate : AuditableEntity
    {
        public string Content { get; set; }
        public int QuestionNumber { get; set; }
    }
}
