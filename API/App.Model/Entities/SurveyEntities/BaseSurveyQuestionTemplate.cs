namespace App.Model.Entities
{
    public abstract class BaseSurveyQuestionTemplate : AuditableEntity
    {
        public SurveyTemplate Survey { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public bool IsRequired { get; set; }
    }
}
