namespace App.Model.Entities
{
    public abstract class BaseQuestionTemplate : AuditableEntity
    {
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public int QuestionNumber { get; set; }
    }
}
