namespace App.Model.Entities
{
    public abstract class BaseQuestionSetEntity : AuditableEntity
    {
        public User Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PagesCount { get; set; }
    }
}
